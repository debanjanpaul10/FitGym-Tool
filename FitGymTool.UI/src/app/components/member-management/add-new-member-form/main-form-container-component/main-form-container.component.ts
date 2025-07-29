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
import {
  MemberManagementConstants,
  ToasterSuccessMessages,
} from '@shared/application.constants';
import { SubscriptionDetailsComponent } from '../subscription-details-component/subscription-details.component';
import { AddMemberDto } from '@models/DTO/members/add-member-dto.model';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { PersonalDetailsComponent } from '../personal-details-component/personal-details.component';
import { FinalValidationFormComponent } from '../final-validation-form/final-validation-form.component';
import { MembersApiService } from '@services/members-api.service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@core/services/toaster.service';

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

  private readonly _dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly _membersApiService: MembersApiService =
    inject(MembersApiService);
  private readonly _loaderService: LoaderService = inject(LoaderService);
  private readonly _toasterService: ToasterService = inject(ToasterService);

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
    this.visible = this._dialogPopupService.isAddMemberDialogOpen;
  }

  protected onNewMemberDataChange(addMemberData: AddMemberDto): void {
    this.newMemberData.set(addMemberData);
  }

  protected submitNewUserData(isSubmit: boolean): void {
    if (isSubmit) {
      this._loaderService.loadingOn();

      this._membersApiService
        .AddNewMemberAsync_FromAdmin(this.newMemberData())
        .subscribe({
          next: (response: ResponseDto) => {
            if (response?.isSuccess && response?.responseData) {
              this._toasterService.showSuccess(
                ToasterSuccessMessages.MemberManagement.AddMemberSuccess
              );
              this.visible.set(false);
            } else {
              this._toasterService.showError(response?.responseData);
              console.error(response?.responseData);
            }
          },
          error: (error: Error) => {
            this._loaderService.loadingOff();
            this._toasterService.showError(error?.message);
            console.error(error);
          },
          complete: () => {
            this._loaderService.loadingOff();
          },
        });
    }
  }
}
