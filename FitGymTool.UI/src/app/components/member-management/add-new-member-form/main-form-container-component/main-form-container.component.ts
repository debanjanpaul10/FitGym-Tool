import {
  Component,
  inject,
  signal,
  WritableSignal,
  Input,
  computed,
} from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { StepperModule } from 'primeng/stepper';

import { DialogPopupService } from '@core/services/dialog-popup.service';
import { MemberManagementConstants } from '@shared/application.constants';
import { SubscriptionDetailsComponent } from '../subscription-details-component/subscription-details.component';
import { AddMemberDto } from '@models/DTO/members/add-member-dto.model';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { PersonalDetailsComponent } from '../personal-details-component/personal-details.component';
import { FinalValidationFormComponent } from '../final-validation-form/final-validation-form.component';

/**
 * Component for adding a new gym member. Handles form creation, validation, membership status mapping, and submission logic.
 */
@Component({
  selector: 'app-main-form-container',
  imports: [
    DialogModule,
    StepperModule,
    SubscriptionDetailsComponent,
    PersonalDetailsComponent,
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
  protected newMemberData: WritableSignal<AddMemberDto> = signal(
    new AddMemberDto()
  );

  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);

  // Computed property to check if first step data is valid
  protected isFirstStepDataValid = computed(() => {
    const data = this.newMemberData();
    return !!(
      data.memberName &&
      data.memberPhoneNumber &&
      data.memberAddress &&
      data.memberDateOfBirth &&
      data.memberGender &&
      data.memberJoinDate &&
      data.membershipStatus
    );
  });

  constructor() {
    this.visible = this.dialogPopupService.isAddMemberDialogOpen;
  }

  protected onNewMemberDataChange(addMemberData: AddMemberDto): void {
    this.newMemberData.set(addMemberData);
  }

  protected submitNewUserData(): void {}
}
