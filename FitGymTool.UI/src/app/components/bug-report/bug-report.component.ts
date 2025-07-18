import {
  Component,
  effect,
  inject,
  OnDestroy,
  signal,
  WritableSignal,
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

import { DialogPopupService } from '@core/services/dialog-popup.service';
import { CommonApiService } from '@services/common-api.service';
import { LoaderService } from '@core/services/loader.service';
import { AddBugReportDto } from '@models/DTO/add-bug-report-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ToasterService } from '@core/services/toaster.service';
import {
  CommonApplicationConstants,
  ToasterSuccessMessages,
} from '@shared/application.constants';
import { CommonService } from '@core/services/common.service';
import { BugSeverityMappingDto } from '@models/DTO/Mapping/bug-severity-mapping-dto.model';

/**
 * Component for submitting bug reports. Handles form creation, validation, severity mapping, and submission logic.
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
  ],
  templateUrl: './bug-report.component.html',
  styleUrl: './bug-report.component.scss',
})
export class BugReportComponent implements OnDestroy {
  protected visible: WritableSignal<boolean> = signal(false);
  protected bugReportForm: FormGroup;
  protected bugReportConstants = CommonApplicationConstants.BugReportConstants;
  protected bugSeverityMappingOptions: BugSeverityMappingDto[] = [];

  private mappingMasterDataSubscription: any;

  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly formBuilder: FormBuilder = inject(FormBuilder);
  private readonly commonApiService: CommonApiService =
    inject(CommonApiService);
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly toasterService: ToasterService = inject(ToasterService);
  private readonly commonService: CommonService = inject(CommonService);

  constructor() {
    this.visible = this.dialogPopupService.isBugReportDialogOpen;
    this.bugReportForm = this.createForm();

    effect(() => {
      if (this.visible()) {
        this.mappingMasterDataSubscription =
          this.commonService.subscribeToMapping(
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
        if (this.mappingMasterDataSubscription) {
          this.mappingMasterDataSubscription.unsubscribe();
        }
      }
    });
  }

  ngOnDestroy(): void {
    if (this.mappingMasterDataSubscription) {
      this.mappingMasterDataSubscription.unsubscribe();
    }
  }

  /**
   * Handles the bug report form submission, sends data to the API, and manages loader and toast notifications.
   */
  protected submitBugReportForm(): void {
    this.loaderService.loadingOn();
    if (this.bugReportForm.valid) {
      const bugReportData: AddBugReportDto = {
        bugTitle: this.bugReportForm.value.bugTitle,
        bugDescription: this.bugReportForm.value.bugDescription,
        bugSeverity: this.bugReportForm.value.bugSeverity,
        createdBy: '',
      };

      this.commonApiService.AddBugReportDataAsync(bugReportData).subscribe({
        next: (response: ResponseDto) => {
          if (response.isSuccess && response.responseData) {
            this.toasterService.showSuccess(
              ToasterSuccessMessages.Common.BugReportSubmitSuccess
            );
            this.resetAndCloseForm();
          } else {
            this.toasterService.showError(response.responseData);
          }
        },
        error: (err: Error) => {
          this.loaderService.loadingOff();
          console.error(err.message);
          this.toasterService.showError(err.message);
        },
        complete: () => {
          this.loaderService.loadingOff();
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
  }

  /**
   * Creates and returns the bug report form group with validation rules.
   * @returns {FormGroup} The initialized bug report form group.
   */
  private createForm(): FormGroup {
    return this.formBuilder.group({
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
    });
  }

  /**
   * Fetches the master mappings data for bug severity from the API.
   */
  private getMasterMappingsData(): void {
    this.loaderService.loadingOn();
    this.commonApiService.GetMappingsMasterDataAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response && response.isSuccess) {
          this.bugSeverityMappingOptions =
            response.responseData?.bugSeverityMapping;
          this.setDefaultSeverity();
        }
      },
      error: (err: Error) => {
        this.loaderService.loadingOff();
        console.error(err);
        this.toasterService.showError(err.message);
      },
      complete: () => {
        this.loaderService.loadingOff();
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
}
