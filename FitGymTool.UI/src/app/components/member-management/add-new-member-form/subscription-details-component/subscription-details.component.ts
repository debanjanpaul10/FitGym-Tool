import {
  Component,
  EventEmitter,
  inject,
  Input,
  Output,
  signal,
  WritableSignal,
  computed,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { IftaLabel } from 'primeng/iftalabel';
import { Select } from 'primeng/select';
import { Button } from 'primeng/button';

import { MemberDetailsDto } from '@models/DTO/members/memberdetails-dto.model';
import { AddMemberDto } from '@models/DTO/members/add-member-dto.model';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { CurrentFeesStructureComponent } from '@components/fees-management/current-fees-structure/current-fees-structure.component';

/**
 * @component
 * Component responsible for handling the second step of the member registration process.
 * Manages subscription details selection including fees duration type and payment structure.
 * Implements multi-step validation ensuring both current step and previous step data are valid
 * before allowing progression to the final validation step. Displays current fees structure
 * and provides form controls for selecting subscription duration options.
 */
@Component({
  selector: 'app-subscription-details',
  imports: [
    IftaLabel,
    Select,
    Button,
    ReactiveFormsModule,
    CurrentFeesStructureComponent,
  ],
  templateUrl: './subscription-details.component.html',
  styleUrl: './subscription-details.component.scss',
})
export class SubscriptionDetailsComponent {
  @Input() masterMappingData: MasterMappingDataDto = new MasterMappingDataDto();
  @Input() set currentMemberData(value: AddMemberDto) {
    this._currentMemberData.set(value);
  }
  get currentMemberData(): AddMemberDto {
    return this._currentMemberData();
  }
  @Input() currentStep: number = 2;
  @Input() visible: boolean = false;
  @Output() currentStepChange = new EventEmitter<number>();
  @Output() newMemberData: EventEmitter<AddMemberDto> =
    new EventEmitter<AddMemberDto>();
  @Output() visibleChange: EventEmitter<boolean> = new EventEmitter<boolean>();

  protected memberDetailsData: WritableSignal<MemberDetailsDto> = signal(
    new MemberDetailsDto()
  );
  protected feesPaymentDurationForm: FormGroup;
  private _currentMemberData: WritableSignal<AddMemberDto> = signal(
    new AddMemberDto()
  );
  private _formValid: WritableSignal<boolean> = signal(false);

  private readonly _formBuilder: FormBuilder = inject(FormBuilder);

  protected isNextButtonEnabled = computed(() => {
    const firstStepValid = this.isFirstStepValid();
    const formValid = this._formValid();
    return firstStepValid && formValid;
  });

  constructor() {
    this.feesPaymentDurationForm = this.createForm();

    // Track form validity changes
    this.feesPaymentDurationForm.statusChanges.subscribe(() => {
      this._formValid.set(this.feesPaymentDurationForm.valid);
    });

    // Initialize form validity
    this._formValid.set(this.feesPaymentDurationForm.valid);
  }

  /**
   * Processes the subscription details form submission and advances to the next step.
   * Validates both the current form and previous step data before proceeding.
   * Updates the member data with the selected fees duration and emits the changes to parent component.
   * Advances the current step to 3 and notifies parent components of the step change.
   */
  protected submitMemberFeesPaymentDurationForm(): void {
    if (this.isNextButtonEnabled()) {
      this.currentStep = 3;
      const updatedData = { ...this._currentMemberData() };
      updatedData.feesDurationTypeName =
        this.feesPaymentDurationForm.value.feesDurationTypeName;

      this.currentStepChange.emit(this.currentStep);
      this.newMemberData.emit(updatedData);
    }
  }

  /**
   * Handles the cancellation of the subscription details form.
   * Resets the form to its initial state, closes the dialog by setting visibility to false,
   * and notifies the parent component about the visibility change.
   */
  protected onCancel(): void {
    this.feesPaymentDurationForm.reset();
    this.visible = false;
    this.visibleChange.emit(false);
  }

  /**
   * Creates and configures the reactive form for subscription details selection.
   * Initializes the form with fees duration type field and applies required validation.
   * Returns a FormGroup instance ready for use in the component template.
   */
  private createForm(): FormGroup {
    return this._formBuilder.group({
      feesDurationTypeName: ['', [Validators.required]],
    });
  }

  /**
   * Validates whether the first step of the member registration process contains complete and valid data.
   * Checks for the presence of all required fields including member name, phone number, address,
   * date of birth, gender, join date, and membership status.
   * Returns true if all required first step fields are populated, false otherwise.
   */
  private isFirstStepValid(): boolean {
    const data = this._currentMemberData();
    const isValid = !!(
      data.memberName &&
      data.memberPhoneNumber &&
      data.memberAddress &&
      data.memberDateOfBirth &&
      data.memberGender &&
      data.memberJoinDate &&
      data.membershipStatus
    );

    return isValid;
  }
}
