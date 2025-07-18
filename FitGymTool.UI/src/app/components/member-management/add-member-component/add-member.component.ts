import {
  Component,
  inject,
  OnDestroy,
  OnInit,
  signal,
  WritableSignal,
  Output,
  EventEmitter,
  Input,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { SelectModule } from 'primeng/select';
import { TextareaModule } from 'primeng/textarea';

import { AddMemberDto } from '@models/DTO/add-member-dto.model';
import { DialogPopupService } from '@core/services/dialog-popup.service';
import {
  MemberManagementConstants,
  ToasterSuccessMessages,
} from '@shared/application.constants';
import { MembershipStatusMappingDto } from '@models/DTO/Mapping/membership-status-mapping-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@core/services/toaster.service';
import { MembersApiService } from '@services/members-api.service';

/**
 * Component for adding a new gym member. Handles form creation, validation, membership status mapping, and submission logic.
 */
@Component({
  selector: 'app-add-member-component',
  imports: [
    DialogModule,
    ButtonModule,
    ReactiveFormsModule,
    InputTextModule,
    TextareaModule,
    DatePickerModule,
    SelectModule,
  ],
  templateUrl: './add-member.component.html',
  styleUrl: './add-member.component.scss',
})
export class AddMemberComponent {
  @Input() membershipStatusOptions: MembershipStatusMappingDto[] = [];
  @Output() memberAdded: EventEmitter<void> = new EventEmitter<void>();

  protected visible: WritableSignal<boolean> = signal(false);
  protected memberForm: FormGroup;
  protected addMemberConstants =
    MemberManagementConstants.AddNewMemberConstants;
  protected genderOptions = this.addMemberConstants.genderOptions;

  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly formBuilder: FormBuilder = inject(FormBuilder);
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly toasterService: ToasterService = inject(ToasterService);
  private readonly membersApiService: MembersApiService =
    inject(MembersApiService);

  constructor() {
    this.visible = this.dialogPopupService.isAddMemberDialogOpen;
    this.memberForm = this.createForm();
  }

  /**
   * Handles the add member form submission, sends data to the API, and manages loader and toast notifications.
   */
  protected submitNewUserForm(): void {
    this.loaderService.loadingOn();
    if (this.memberForm.valid) {
      const memberData: AddMemberDto = {
        memberName: this.memberForm.value.memberName,
        memberEmail: this.memberForm.value.memberEmail || null,
        memberPhoneNumber: this.memberForm.value.memberPhoneNumber,
        memberAddress: this.memberForm.value.memberAddress,
        memberDateOfBirth: this.memberForm.value.memberDateOfBirth,
        memberGender: this.memberForm.value.memberGender,
        memberJoinDate: this.memberForm.value.memberJoinDate,
        membershipStatus: this.memberForm.value.membershipStatus,
      };

      this.membersApiService.AddNewMemberAsync_FromAdmin(memberData).subscribe({
        next: (response: ResponseDto) => {
          if (response.isSuccess && response.responseData) {
            this.toasterService.showSuccess(
              ToasterSuccessMessages.MemberManagement.AddMemberSuccess
            );
            this.memberAdded.emit();
            this.onCancel();
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
   * Resets the add member form and closes the dialog.
   */
  protected onCancel(): void {
    this.memberForm.reset();
    this.memberForm.patchValue({
      memberJoinDate: new Date(),
    });
    this.visible.set(false);
  }

  /**
   * Creates and returns the add member form group with validation rules.
   * @returns {FormGroup} The initialized add member form group.
   */
  private createForm(): FormGroup {
    return this.formBuilder.group({
      memberName: ['', [Validators.required, Validators.minLength(2)]],
      memberEmail: ['', [Validators.email]],
      memberPhoneNumber: [
        '',
        [Validators.required, Validators.pattern(/^\d{10,15}$/)],
      ],
      memberAddress: ['', [Validators.required, Validators.minLength(5)]],
      memberDateOfBirth: [null, [Validators.required]],
      memberGender: ['', [Validators.required]],
      memberJoinDate: [new Date(), [Validators.required]],
      membershipStatus: ['', [Validators.required]],
    });
  }
}
