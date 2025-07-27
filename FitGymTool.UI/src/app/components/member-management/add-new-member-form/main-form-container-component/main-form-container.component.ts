import {
  Component,
  inject,
  signal,
  WritableSignal,
  Input,
} from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { StepperModule } from 'primeng/stepper';

import { DialogPopupService } from '@core/services/dialog-popup.service';
import { MemberManagementConstants } from '@shared/application.constants';
import { SubscriptionDetailsSelection } from '../subscription-details-selection/subscription-details-selection.component';
import { NewMemberCreationComponent } from '../new-member-creation/new-member-creation.component';
import { AddMemberDto } from '@models/DTO/members/add-member-dto.model';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { FinalValidationFormComponent } from '../final-validation-form/final-validation-form.component';

/**
 * Component for adding a new gym member. Handles form creation, validation, membership status mapping, and submission logic.
 */
@Component({
  selector: 'app-main-form-container',
  imports: [
    DialogModule,
    StepperModule,
    SubscriptionDetailsSelection,
    NewMemberCreationComponent,
    FinalValidationFormComponent,
  ],
  templateUrl: './main-form-container.component.html',
  styleUrl: './main-form-container.component.scss',
})
export class MainFormContainerComponent {
  @Input() masterMappingData: MasterMappingDataDto = new MasterMappingDataDto();

  protected addMemberConstants =
    MemberManagementConstants.AddNewMemberConstants;
  protected visible: WritableSignal<boolean> = signal(false);
  protected currentStep: WritableSignal<number> = signal(1);
  protected newMemberData: AddMemberDto = new AddMemberDto();

  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);

  constructor() {
    this.visible = this.dialogPopupService.isAddMemberDialogOpen;
  }

  protected onNewMemberDataChange(addMemberData: AddMemberDto): void {
    this.newMemberData = addMemberData;
  }

  protected submitNewUserData(): void {}
}
