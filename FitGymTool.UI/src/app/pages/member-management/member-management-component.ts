import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { MembersApiService } from '@core/services/members-api-service';
import { LoaderService } from '@services/loader.service';
import { ToasterService } from '@services/toaster-service';

/**
 * Component responsible for managing gym members, including fetching and displaying member data.
 * Utilizes MembersApiService to retrieve member information, LoaderService to indicate loading state,
 * and ToasterService to display error messages.
 */
@Component({
  selector: 'app-member-management',
  imports: [],
  templateUrl: './member-management-component.html',
  styleUrl: './member-management-component.scss',
})
export class MemberManagementComponent implements OnInit {
  private readonly membersApiService: MembersApiService =
    inject(MembersApiService);
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly toasterService: ToasterService = inject(ToasterService);

  private allUsersData: WritableSignal<any> = signal(null);

  /**
   * Angular lifecycle hook that is called after component initialization.
   * Initiates the process to fetch all member data from the API.
   */
  ngOnInit(): void {
    this.getAllMembersData();
  }

  // #region PRIVATE Methods

  /**
   * Fetches all member data from the backend API.
   * Shows a loading indicator while the request is in progress.
   * On success, updates the allUsersData signal with the retrieved data.
   * On error, hides the loading indicator and displays an error message using the toaster service.
   * Hides the loading indicator when the request completes.
   *
   * @private
   */
  private getAllMembersData(): void {
    this.loaderService.loadingOn();

    this.membersApiService.GetAllMembersAsync().subscribe({
      next: (response) => {
        if (response && response?.isSuccess) {
          this.allUsersData.set(response.responseData);
        } else {
          this.toasterService.showError(response?.responseData);
        }
      },
      error: (err) => {
        this.loaderService.loadingOff();
        console.error(err);
        this.toasterService.showError(err?.message);
      },
      complete: () => {
        this.loaderService.loadingOff();
      },
    });
  }

  // #endregion
}
