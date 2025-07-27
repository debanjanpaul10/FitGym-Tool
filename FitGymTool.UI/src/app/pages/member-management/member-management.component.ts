import { CommonModule } from '@angular/common';
import {
  Component,
  inject,
  OnDestroy,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { SkeletonModule } from 'primeng/skeleton';
import { ButtonModule } from 'primeng/button';

import { MembersListComponent } from '@components/member-management/members-list-component/members-list.component';
import { MembersApiService } from '@services/members-api.service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ToasterService } from '@core/services/toaster.service';
import { MemberDetailsDto } from '@models/DTO/members/memberdetails-dto.model';
import { MemberManagementConstants } from '@shared/application.constants';
import { DialogPopupService } from '@core/services/dialog-popup.service';
import { MainFormContainerComponent } from '@components/member-management/add-new-member-form/main-form-container-component/main-form-container.component';
import { CommonService } from '@core/services/common.service';
import { UpdateMembershipStatusComponent } from '@components/member-management/update-membership-status-component/update-membership-status.component';
import { MembershipStatusMappingDto } from '@models/DTO/Mapping/membership-status-mapping-dto.model';
import { LoaderService } from '@core/services/loader.service';
import { CommonApiService } from '@services/common-api.service';
import { EditMemberComponent } from '@components/member-management/edit-member-component/edit-member.component';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';

/**
 * Component responsible for managing gym members, including fetching and displaying member data.
 * Utilizes MembersApiService to retrieve member information, LoaderService to indicate loading state,
 * and ToasterService to display error messages.
 */
@Component({
  selector: 'app-member-management',
  imports: [
    CommonModule,
    MembersListComponent,
    SkeletonModule,
    ButtonModule,
    MainFormContainerComponent,
    UpdateMembershipStatusComponent,
    EditMemberComponent,
  ],
  templateUrl: './member-management.component.html',
  styleUrl: './member-management.component.scss',
})
export class MemberManagementComponent implements OnInit, OnDestroy {
  protected MemberDashboardConstants =
    MemberManagementConstants.MembersDashboardConstants;
  protected allUsersData: WritableSignal<MemberDetailsDto[] | null> =
    signal(null);
  protected isUsersDataLoading: WritableSignal<boolean> = signal(false);
  protected membershipStatusOptions: MembershipStatusMappingDto[] = [];
  protected masterMappingData: MasterMappingDataDto =
    new MasterMappingDataDto();

  private masterMappingDataSubscription: any;

  private readonly membersApiService: MembersApiService =
    inject(MembersApiService);
  private readonly toasterService: ToasterService = inject(ToasterService);
  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly commonService: CommonService = inject(CommonService);
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly commonApiService: CommonApiService =
    inject(CommonApiService);

  ngOnInit(): void {
    this.commonService.MemberDetailsData.subscribe(
      (data: MemberDetailsDto[] | null) => {
        if (data && Object.values(data).length > 0) {
          this.allUsersData.set(data);
        } else {
          this.getAllMembersData();
        }
      }
    );

    this.handleMappingData();
  }

  ngOnDestroy(): void {
    if (this.masterMappingDataSubscription) {
      this.masterMappingDataSubscription.unsubscribe();
    }
  }

  protected handleAddNewMember(): void {
    this.dialogPopupService.openAddMemberDialog();
  }

  protected handleTerminateMember(): void {
    this.dialogPopupService.openMembershipStatusDialog();
  }

  protected handleEditMember(): void {
    this.dialogPopupService.openMemberUpdateDetailsDialog();
  }

  protected onMemberUpdated(): void {
    this.getAllMembersData();
  }

  protected refreshMasterMappingData(): void {
    this.getMasterMappingsData();
  }

  // #region PRIVATE Methods

  /**
   * Fetches all member data from the backend API.
   * Shows a loading indicator while the request is in progress.
   * On success, updates the allUsersData signal with the retrieved data.
   * On error, hides the loading indicator and displays an error message using the toaster service.
   * Hides the loading indicator when the request completes.
   */
  private getAllMembersData(): void {
    this.isUsersDataLoading.set(true);

    this.membersApiService.GetAllMembersAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response && response?.isSuccess) {
          this.allUsersData.set(response.responseData);
        } else {
          this.toasterService.showError(response?.responseData);
        }
      },
      error: (err: any) => {
        this.isUsersDataLoading.set(false);
        console.error(err);
        this.toasterService.showError(err?.message);
      },
      complete: () => {
        this.isUsersDataLoading.set(false);
      },
    });
  }

  /**
   * Fetches the master mappings data for membership status from the API.
   */
  private getMasterMappingsData(): void {
    this.loaderService.loadingOn();
    this.commonApiService.GetMappingsMasterDataAsync().subscribe({
      next: (response: ResponseDto) => {
        if (response?.isSuccess && response?.responseData) {
          this.masterMappingData = response.responseData;
          // Update the common service with the fetched data
          this.commonService.MappingMasterData = response.responseData;
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
   * Checks if the master mapping data contains valid data
   */
  private checkValidMappingDataExists(data: MasterMappingDataDto): boolean {
    return (
      data &&
      ((data.membershipStatusMapping &&
        data.membershipStatusMapping.length > 0) ||
        (data.membershipStatusMapping &&
          data.membershipStatusMapping.length > 0) ||
        Object.keys(data).some((key) => {
          const value = (data as any)[key];
          return Array.isArray(value) && value.length > 0;
        }))
    );
  }

  private handleMappingData(): void {
    this.masterMappingDataSubscription =
      this.commonService.MappingMasterData.subscribe(
        (data: MasterMappingDataDto) => {
          if (this.checkValidMappingDataExists(data)) {
            this.masterMappingData = data;
          } else {
            this.getMasterMappingsData();
          }
        }
      );

    this.commonService.subscribeToMapping(
      'membershipStatusMapping',
      (options) => {
        this.membershipStatusOptions = options as MembershipStatusMappingDto[];
      },
      () => {
        this.getMasterMappingsData();
      }
    );
  }

  // #endregion
}
