import { Component, inject, Input, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IftaLabel } from 'primeng/iftalabel';
import { DatePicker } from 'primeng/datepicker';
import { Select } from 'primeng/select';
import { Button } from 'primeng/button';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { InputText } from 'primeng/inputtext';
import { Textarea } from 'primeng/textarea';

import { MemberManagementConstants } from '@shared/application.constants';
import { AddMemberDto } from '@models/DTO/members/add-member-dto.model';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';

/**
 * @component
 * Component responsible for handling the first step of the member registration process.
 * Manages the collection of essential member information including personal details,
 * contact information, and membership preferences. Implements comprehensive form validation
 * with required field checks, email format validation, and phone number pattern matching.
 * Serves as the entry point for the multi-step member registration workflow.
 */
@Component({
  selector: 'app-personal-details',
  imports: [
    CommonModule,
    IftaLabel,
    DatePicker,
    Select,
    Button,
    ReactiveFormsModule,
    InputText,
    Textarea,
  ],
  templateUrl: './personal-details.component.html',
  styleUrl: './personal-details.component.scss',
})
export class PersonalDetailsComponent {
  @Input() masterMappingData: MasterMappingDataDto = new MasterMappingDataDto();
  @Input() visible: boolean = false;
  @Input() currentStep: number = 1;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() currentStepChange = new EventEmitter<number>();
  @Output() newMemberData: EventEmitter<AddMemberDto> =
    new EventEmitter<AddMemberDto>();

  protected memberForm: FormGroup;
  protected addMemberConstants =
    MemberManagementConstants.AddNewMemberConstants;
  protected genderOptions = this.addMemberConstants.genderOptions;

  private readonly formBuilder: FormBuilder = inject(FormBuilder);

  // Getter to check if next button should be enabled
  protected get isNextButtonEnabled(): boolean {
    return this.memberForm.valid;
  }

  // Helper method to get invalid control names for debugging
  protected getInvalidControls(): string[] {
    const invalidControls: string[] = [];
    Object.keys(this.memberForm.controls).forEach((key) => {
      const control = this.memberForm.get(key);
      if (control && control.invalid) {
        invalidControls.push(key);
      }
    });
    return invalidControls;
  }

  constructor() {
    this.memberForm = this.createForm();
  }

  /**
   * Processes the new member form submission and advances to the subscription details step.
   * Validates the form data and constructs a complete AddMemberDto object with all collected information.
   * Advances the workflow to step 2 and emits the member data to parent components for further processing.
   * Only proceeds if all form validations pass successfully.
   */
  protected submitNewUserForm(): void {
    // Check individual field validity
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
        feesDurationTypeName: this.memberForm.value.feesDurationTypeName,
      };

      this.currentStep = 2; // Move to step 2
      this.currentStepChange.emit(this.currentStep);
      this.newMemberData.emit(memberData);
    }
  }

  /**
   * Handles the cancellation of the new member creation process.
   * Resets all form fields to their initial state, restores the join date to current date,
   * closes the dialog by setting visibility to false, and notifies parent components of the change.
   */
  protected onCancel(): void {
    this.memberForm.reset();
    this.memberForm.patchValue({
      memberJoinDate: new Date(),
    });
    this.visible = false;
    this.visibleChange.emit(this.visible);
  }

  /**
   * Creates and configures the reactive form for new member registration with comprehensive validation rules.
   * Initializes form controls for all member fields including name, email, phone, address, dates, gender, and membership status.
   * Applies appropriate validators such as required fields, minimum length, email format, and phone number pattern matching.
   * Sets default values including current date for join date and empty string for optional fields.
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
      feesDurationTypeName: [''],
    });
  }
}
