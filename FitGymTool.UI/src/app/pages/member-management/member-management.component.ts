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

import { MembersListComponent } from '@components/member-management/members-list-component/members-list.component';
import { MembersApiService } from '@services/members-api.service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ToasterService } from '@core/services/toaster.service';
import { ButtonModule } from 'primeng/button';
import { MemberDetailsDto } from '@models/DTO/members/memberdetails-dto.model';
import { MemberManagementConstants } from '@shared/application.constants';
import { DialogPopupService } from '@core/services/dialog-popup.service';
import { AddMemberComponent } from '@components/member-management/add-member-component/add-member.component';
import { CommonService } from '@core/services/common.service';
import { UpdateMembershipStatusComponent } from '@components/member-management/update-membership-status-component/update-membership-status.component';
import { MembershipStatusMappingDto } from '@models/DTO/Mapping/membership-status-mapping-dto.model';
import { LoaderService } from '@core/services/loader.service';
import { CommonApiService } from '@services/common-api.service';
import { EditMemberComponent } from '@components/member-management/edit-member-component/edit-member.component';

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
    AddMemberComponent,
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

  private mappingMasterDataSubscription: any;

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

    this.mappingMasterDataSubscription = this.commonService.subscribeToMapping(
      'membershipStatusMapping',
      (options) => {
        this.membershipStatusOptions = options as MembershipStatusMappingDto[];
      },
      () => {
        this.getMasterMappingsData();
      }
    );
  }

  ngOnDestroy(): void {
    if (this.mappingMasterDataSubscription) {
      this.mappingMasterDataSubscription.unsubscribe();
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
        if (response && response.isSuccess) {
          this.membershipStatusOptions =
            response.responseData?.membershipStatusMapping;
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

  // #endregion
}
