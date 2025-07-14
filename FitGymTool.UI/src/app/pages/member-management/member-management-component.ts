import { CommonModule } from '@angular/common';
import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { SkeletonModule } from 'primeng/skeleton';

import { MembersListComponent } from '@components/member-management/members-list-component/members-list-component';
import { MembersApiService } from '@core/services/members-api-service';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ToasterService } from '@services/toaster-service';
import { ButtonModule } from 'primeng/button';
import { MemberDetailsDto } from '@models/DTO/memberdetails-dto.model';
import { MemberManagementConstants } from '@shared/application.constants';

/**
 * Component responsible for managing gym members, including fetching and displaying member data.
 * Utilizes MembersApiService to retrieve member information, LoaderService to indicate loading state,
 * and ToasterService to display error messages.
 */
@Component({
  selector: 'app-member-management',
  imports: [CommonModule, MembersListComponent, SkeletonModule, ButtonModule],
  templateUrl: './member-management-component.html',
  styleUrl: './member-management-component.scss',
})
export class MemberManagementComponent implements OnInit {
  public MemberDashboardConstants =
    MemberManagementConstants.MembersDashboardConstant;

  public allUsersData: WritableSignal<MemberDetailsDto[] | null> = signal(null);
  public isUsersDataLoading: WritableSignal<boolean> = signal(true);

  private readonly membersApiService: MembersApiService =
    inject(MembersApiService);
  private readonly toasterService: ToasterService = inject(ToasterService);

  /**
   * Angular lifecycle hook that is called after component initialization.
   * Initiates the process to fetch all member data from the API.
   */
  ngOnInit(): void {
    this.getAllMembersData();
  }

  public handleAddNewUser(): void {
    alert('Feature being actively worked on');
  }

  public handleTerminateUser(): void {
    alert('Feature will be worked on soon!');
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

  // #endregion
}
