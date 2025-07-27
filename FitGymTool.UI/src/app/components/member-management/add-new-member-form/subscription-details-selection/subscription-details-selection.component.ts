import {
  Component,
  EventEmitter,
  inject,
  Input,
  Output,
  signal,
  WritableSignal,
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

@Component({
  selector: 'app-subscription-details-selection',
  imports: [
    IftaLabel,
    Select,
    Button,
    ReactiveFormsModule,
    CurrentFeesStructureComponent,
  ],
  templateUrl: './subscription-details-selection.component.html',
  styleUrl: './subscription-details-selection.component.scss',
})
export class SubscriptionDetailsSelection {
  @Input() masterMappingData: MasterMappingDataDto = new MasterMappingDataDto();
  @Input() currentMemberData: AddMemberDto = new AddMemberDto();
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

  private readonly _formBuilder: FormBuilder = inject(FormBuilder);

  constructor() {
    this.feesPaymentDurationForm = this.createForm();
  }

  protected submitMemberFeesPaymentDurationForm(): void {
    if (this.feesPaymentDurationForm.valid) {
      this.currentStep = 3;
      this.currentMemberData.feesDurationTypeName =
        this.feesPaymentDurationForm.value.feesDurationTypeName;

      this.currentStepChange.emit(this.currentStep);
      this.newMemberData.emit(this.currentMemberData);
    }
  }

  protected onCancel(): void {
    this.feesPaymentDurationForm.reset();
    this.visible = false;
    this.visibleChange.emit(false);
  }

  private createForm(): FormGroup {
    return this._formBuilder.group({
      feesDurationTypeName: ['', [Validators.required]],
    });
  }
}
