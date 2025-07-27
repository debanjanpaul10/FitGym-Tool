import { Component, inject, Input, EventEmitter, Output } from '@angular/core';
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

@Component({
  selector: 'app-new-member-creation',
  imports: [
    IftaLabel,
    DatePicker,
    Select,
    Button,
    ReactiveFormsModule,
    InputText,
    Textarea,
  ],
  templateUrl: './new-member-creation.component.html',
  styleUrl: './new-member-creation.component.scss',
})
export class NewMemberCreationComponent {
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

  constructor() {
    this.memberForm = this.createForm();
  }

  /**
   * Handles the add member form submission, sends data to the API, and manages loader and toast notifications.
   */
  protected submitNewUserForm(): void {
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
        feesDurationTypeName: '',
      };

      this.currentStep = 2; // Move to step 2
      this.currentStepChange.emit(this.currentStep);
      this.newMemberData.emit(memberData);
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
    this.visible = false;
    this.visibleChange.emit(this.visible);
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
