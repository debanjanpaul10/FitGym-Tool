import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { AddMemberDto } from '@models/DTO/members/add-member-dto.model';

@Component({
  selector: 'app-final-validation-form',
  imports: [],
  templateUrl: './final-validation-form.component.html',
  styleUrl: './final-validation-form.component.scss',
})
export class FinalValidationFormComponent {
  @Input() masterMappingData: MasterMappingDataDto = new MasterMappingDataDto();
  @Input() currentMemberData: AddMemberDto = new AddMemberDto();
  @Input() currentStep: number = 2;
  @Input() visible: boolean = false;
  @Output() currentStepChange = new EventEmitter<number>();
  @Output() newMemberData: EventEmitter<AddMemberDto> =
    new EventEmitter<AddMemberDto>();
  @Output() visibleChange: EventEmitter<boolean> = new EventEmitter<boolean>();
}
