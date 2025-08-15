import {
  Component,
  effect,
  inject,
  OnDestroy,
  signal,
  WritableSignal,
  ChangeDetectorRef,
  OnInit,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { TextareaModule } from 'primeng/textarea';
import { IftaLabelModule } from 'primeng/iftalabel';
import { MessageModule } from 'primeng/message';

import { DialogPopupService } from '@core/services/dialog-popup.service';
import { CommonApiService } from '@services/common-api.service';
import { LoaderService } from '@core/services/loader.service';
import { AddBugReportDTO } from '@models/DTO/add-bug-report-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ToasterService } from '@core/services/toaster.service';
import {
  CommonApplicationConstants,
  ToasterSuccessMessages,
} from '@shared/application.constants';
import { CommonService } from '@core/services/common.service';
import { BugSeverityMappingDto } from '@models/DTO/Mapping/bug-severity-mapping-dto.model';
import { BugSeverityInputDTO } from '@models/DTO/bug-severity-input-dto.model';
import { BugSeverityResponseDTO } from '@models/DTO/bug-severity-response-dto.model';

/**
 * Component for submitting bug reports. Handles form creation, validation, severity mapping, and submission logic.
 *
 * This component provides a dialog-based interface for users to report bugs with the following features:
 * - Reactive form with validation for bug title, description, and severity
 * - Dynamic loading of bug severity options from master data
 * - Automatic page URL capture for context
 * - Form submission with API integration
 * - Loading states and toast notifications
 * - Proper cleanup of subscriptions
 */
@Component({
  selector: 'app-bug-report',
  imports: [
    ButtonModule,
    DialogModule,
    InputTextModule,
    SelectModule,
    TextareaModule,
    ReactiveFormsModule,
    IftaLabelModule,
    MessageModule,
  ],
  templateUrl: './bug-report.component.html',
  styleUrl: './bug-report.component.scss',
})
export class BugReportComponent implements OnDestroy, OnInit {
  protected visible: WritableSignal<boolean> = signal(false);
  protected bugReportForm: FormGroup;
  protected bugReportConstants = CommonApplicationConstants.BugReportConstants;
  protected bugSeverityMappingOptions: BugSeverityMappingDto[] = [];
  protected isBugSeverityPopulated: WritableSignal<boolean> = signal(false);
  protected canGetBugSeverity: WritableSignal<boolean> = signal(false);

  private _mappingMasterDataSubscription: any;

  private readonly _dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly _formBuilder: FormBuilder = inject(FormBuilder);
  private readonly _commonApiService: CommonApiService =
    inject(CommonApiService);
  private readonly _loaderService: LoaderService = inject(LoaderService);
  private readonly _toasterService: ToasterService = inject(ToasterService);
  private readonly _commonService: CommonService = inject(CommonService);
  private readonly _cdr: ChangeDetectorRef = inject(ChangeDetectorRef);

  constructor() {
    this.visible = this._dialogPopupService.isBugReportDialogOpen;
    this.bugReportForm = this.createForm();

    effect(() => {
      if (this.visible()) {
        this.setPageUrl();
        this.isBugSeverityPopulated.set(false);
        this.canGetBugSeverity.set(false);

        this._mappingMasterDataSubscription =
          this._commonService.subscribeToMapping(
            'bugSeverityMapping',
            (options) => {
              this.bugSeverityMappingOptions =
                options as BugSeverityMappingDto[];
            },
            () => {
              this.getMasterMappingsData();
            }
          );
      } else {
        if (this._mappingMasterDataSubscription) {
          this._mappingMasterDataSubscription.unsubscribe();
        }
      }
    });

    this.bugReportForm.valueChanges.subscribe(() => {
      this.updateCanGetBugSeverity();
    });

    this.setPageUrl();
    this.updateCanGetBugSeverity();
  }

  ngOnInit(): void {
    // Initial state: both buttons disabled
    this.canGetBugSeverity.set(false);
    this.isBugSeverityPopulated.set(false);
  }

  ngOnDestroy(): void {
    if (this._mappingMasterDataSubscription) {
      this._mappingMasterDataSubscription.unsubscribe();
    }
  }

  /**
   * Handles the bug report form submission, sends data to the API, and manages loader and toast notifications.
   */
  protected submitBugReportForm(): void {
    this._loaderService.loadingOn();
    if (this.bugReportForm.valid) {
      const bugReportData: AddBugReportDTO = {
        bugTitle: this.bugReportForm.value.bugTitle,
        bugDescription: this.bugReportForm.value.bugDescription,
        bugSeverity: this.bugReportForm.value.bugSeverity,
        createdBy: '',
        pageUrl: window.location.href,
      };

      this._commonApiService.AddBugReportDataAsync(bugReportData).subscribe({
        next: (response: ResponseDto) => {
          if (response.isSuccess && response.responseData) {
            this._toasterService.showSuccess(
              ToasterSuccessMessages.Common.BugReportSubmitSuccess
            );
            this.resetAndCloseForm();
          } else {
            this._toasterService.showError(response.responseData);
          }
        },
        error: (err: Error) => {
          this._loaderService.loadingOff();
          console.error(err.message);
          this._toasterService.showError(err.message);
        },
        complete: () => {
          this._loaderService.loadingOff();
        },
      });
    }
  }

  /**
   * Resets the bug report form and closes the dialog.
   */
  protected resetAndCloseForm(): void {
    this.bugReportForm.reset();
    this.visible.set(false);
    this.isBugSeverityPopulated.set(false);
    this.canGetBugSeverity.set(false);
    this.setPageUrl();
  }

  /**
   * Gets the bug severity status from AI.
   */
  protected getBugSeverityStatus(): void {
    this._loaderService.loadingOn();
    const bugSeverityInput: BugSeverityInputDTO = {
      bugTitle: this.bugReportForm.value.bugTitle,
      bugDescription: this.bugReportForm.value.bugDescription,
    };

    this._commonApiService
      .GetBugSeverityStatusAsync(bugSeverityInput)
      .subscribe({
        next: (response: ResponseDto) => {
          if (response?.isSuccess && response?.responseData) {
            this.populateBugSeverityDropdown(response.responseData);
          } else {
            this._toasterService.showError(response.responseData);
          }
        },
        error: (err: Error) => {
          this._loaderService.loadingOff();
          console.error(err.message);
          this._toasterService.showError(err.message);
        },
        complete: () => {
          this._loaderService.loadingOff();
        },
      });
  }

  // #region PRIVATE METHODS

  /**
   * Creates and returns the bug report form group with validation rules.
   * @returns {FormGroup} The initialized bug report form group.
   */
  private createForm(): FormGroup {
    var formData = this._formBuilder.group({
      bugTitle: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(80),
        ],
      ],
      bugDescription: [
        '',
        [
          Validators.required,
          Validators.minLength(20),
          Validators.maxLength(500),
        ],
      ],
      bugSeverity: [{ value: '', disabled: true }, [Validators.required]],
      pageUrl: ['', [Validators.required]],
    });

    formData.get('bugSeverity')?.disable();
    return formData;
  }

  /**
   * Fetches the master mappings data for bug severity from the API.
   */
  private getMasterMappingsData(): void {
    this._loaderService.loadingOn();
    this._commonApiService.GetMappingsMasterDataAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response && response.isSuccess) {
          this.bugSeverityMappingOptions =
            response.responseData?.bugSeverityMapping;
          this.setDefaultSeverity();
        }
      },
      error: (err: Error) => {
        this._loaderService.loadingOff();
        console.error(err);
        this._toasterService.showError(err.message);
      },
      complete: () => {
        this._loaderService.loadingOff();
      },
    });
  }

  /**
   * Sets the default severity to "Medium" and keeps the field disabled.
   */
  private setDefaultSeverity(): void {
    const mediumSeverity = this.bugSeverityMappingOptions.find(
      (option) => option.severityName.toLowerCase() === 'medium'
    );

    if (mediumSeverity) {
      this.bugReportForm.patchValue({
        bugSeverity: mediumSeverity.id,
      });
    }
  }

  /**
   * Sets the page url.
   */
  private setPageUrl(): void {
    const pageUrl = window.location.pathname;
    this.bugReportForm.patchValue({
      pageUrl: pageUrl,
    });
  }

  /**
   * Updates the canGetBugSeverity signal based on title and description values.
   */
  private updateCanGetBugSeverity(): void {
    const title = this.bugReportForm.get('bugTitle')?.value?.trim();
    const description = this.bugReportForm.get('bugDescription')?.value?.trim();

    this.canGetBugSeverity.set(!!(title && description));
  }

  /**
   * Populates the bug severity dropdown with the AI-suggested severity.
   */
  private populateBugSeverityDropdown(
    severityName: BugSeverityResponseDTO
  ): void {
    const trimmedBugSev = severityName.bugSeverity.trim();
    const matchingSeverity = this.bugSeverityMappingOptions.find(
      (option) =>
        option.severityName.toLowerCase() === trimmedBugSev.toLowerCase()
    );

    if (matchingSeverity) {
      this.bugReportForm.patchValue({
        bugSeverity: matchingSeverity.id,
      });
      this.bugReportForm.get('bugSeverity')?.enable();
    }

    this.isBugSeverityPopulated.set(true);
    this._cdr.detectChanges();
  }

  // #endregion
}
