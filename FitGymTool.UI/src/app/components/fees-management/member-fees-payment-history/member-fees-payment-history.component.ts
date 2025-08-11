import {
  Component,
  computed,
  inject,
  input,
  OnChanges,
  output,
  signal,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { Ripple } from 'primeng/ripple';
import { finalize, switchMap, tap } from 'rxjs/operators';
import { EMPTY, of } from 'rxjs';

import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@core/services/toaster.service';
import { MemberPaymentHistoryDTO } from '@models/DTO/member-payment-history-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { MemberFeesApiService } from '@services/member-fees-api.service';
import { Column } from '@models/interfaces/column.interface';
import { UpdateMemberFeesDTO } from '@models/DTO/update-member-fees-dto.model';
import { ToasterSuccessMessages } from '@shared/application.constants';

/**
 * @component
 * Component for displaying and managing member payment history in a modal dialog.
 * Provides functionality to view payment records, update payment status, and refresh data.
 */
@Component({
  selector: 'app-member-fees-payment-history',
  imports: [DialogModule, ButtonModule, TableModule, CommonModule, Ripple],
  templateUrl: './member-fees-payment-history.component.html',
  styleUrl: './member-fees-payment-history.component.scss',
})
export class MemberFeesPaymentHistoryComponent implements OnChanges {
  readonly memberEmail = input.required<string>();
  readonly onPaymentUpdated = output<void>();
  @ViewChild('paymentHistoryTable') paymentHistoryTable!: Table;

  private readonly _memberFeesApiService = inject(MemberFeesApiService);
  private readonly _loaderService = inject(LoaderService);
  private readonly _toasterService = inject(ToasterService);

  protected readonly currentMemberPaymentHistory = signal<
    MemberPaymentHistoryDTO[]
  >([]);
  protected readonly showDialog = signal(false);
  protected readonly isLoading = signal(false);

  protected readonly columnHeaders: Column[] = [
    { field: 'memberId', header: 'Member ID' },
    { field: 'memberName', header: 'Member Name' },
    { field: 'memberEmail', header: 'Email Address' },
    { field: 'memberStatus', header: 'Membership Status' },
    { field: 'feesPaymentStatus', header: 'Payment Status' },
    { field: 'amount', header: 'Fees Amount' },
    { field: 'fromDate', header: 'From Date' },
    { field: 'toDate', header: 'To Date' },
    { field: 'actions', header: '' },
  ] as const;

  protected readonly sortedPaymentHistory = computed(() =>
    this.sortPaymentHistoryByToDate(this.currentMemberPaymentHistory())
  );

  protected readonly paymentStatusClasses = computed(() => {
    const statusMap = new Map([
      ['paid', 'bg-success text-white'],
      ['pending', 'bg-info text-white'],
      ['overdue', 'bg-danger text-white'],
      ['due', 'bg-warning text-dark'],
    ]);
    return statusMap;
  });

  private _savedScrollPosition = 0;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['memberEmail'] && changes['memberEmail'].currentValue !== '') {
      this.loadPaymentHistory();
    }
  }

  /**
   * Closes the payment history dialog.
   */
  protected closeDialog(): void {
    this.showDialog.set(false);
  }

  /**
   * Refreshes the payment history data by clearing the current table state and reloading data from the API.
   */
  private refreshPaymentHistory(): void {
    const email = this.memberEmail();
    if (email) {
      // Clear the data first to force re-render
      this.currentMemberPaymentHistory.set([]);

      // Use setTimeout to ensure the change detection cycle completes
      setTimeout(() => {
        // Reset table state
        this.paymentHistoryTable?.reset();
        this.paymentHistoryTable?.clear();

        // Reload data from API
        this.getPaymentHistoryDataForMember(email, true);
      }, 100);
    }
  }

  /**
   * Handles dialog show event by restoring the previous scroll position.
   */
  protected onDialogShow(): void {
    this.restoreScrollPosition();
  }

  /**
   * Handles dialog hide event by restoring the previous scroll position.
   */
  protected onDialogHide(): void {
    this.restoreScrollPosition();
  }

  /**
   * Returns the CSS class for payment status badge based on the status value.
   * @param status - The payment status string
   * @returns CSS class string for styling the status badge
   */
  protected getPaymentStatusClass(status: string): string {
    return (
      this.paymentStatusClasses().get(status?.toLowerCase()) ||
      'bg-secondary text-white'
    );
  }

  /**
   * Updates member fees payment status to paid and refreshes the payment history.
   * @param rowData - The payment history record to update
   */
  protected updateMemberFeesData(rowData: MemberPaymentHistoryDTO): void {
    if (this.isLoading()) return;

    const updateData: UpdateMemberFeesDTO = {
      memberEmailAddress: rowData.memberEmail,
      amount: rowData.amount,
      fromDate: rowData.fromDate,
      toDate: rowData.toDate,
    } as UpdateMemberFeesDTO;

    this.isLoading.set(true);
    this._loaderService.loadingOn();

    // Optimistically update the UI
    this.updateRowStatusOptimistically(rowData, 'Paid');

    this._memberFeesApiService
      .UpdateMemberFeesDataAsync(updateData)
      .pipe(
        tap((response: ResponseDto) => {
          if (response?.isSuccess && response?.responseData) {
            this._toasterService.showSuccess(
              ToasterSuccessMessages.MemberManagement.FeesPaidSuccesfully
            );
            // Refresh from server to get the latest data
            this.refreshPaymentHistory();
            this.onPaymentUpdated.emit();
          } else {
            // Revert the optimistic update on failure
            this.refreshPaymentHistory();
            this._toasterService.showError(response?.responseData);
          }
        }),
        finalize(() => {
          this.isLoading.set(false);
          this._loaderService.loadingOff();
        })
      )
      .subscribe({
        error: (error: Error) => {
          // Revert the optimistic update on error
          this.refreshPaymentHistory();
          this._toasterService.showError(error?.message);
          console.error(error);
        },
      });
  }

  /**
   * Optimistically updates a row's payment status in the UI before the API call completes
   * @param rowData - The row data to update
   * @param newStatus - The new payment status
   */
  private updateRowStatusOptimistically(
    rowData: MemberPaymentHistoryDTO,
    newStatus: string
  ): void {
    const currentData = this.currentMemberPaymentHistory();
    const updatedData = currentData.map((item) => {
      if (
        item.memberEmail === rowData.memberEmail &&
        item.fromDate === rowData.fromDate &&
        item.toDate === rowData.toDate &&
        item.amount === rowData.amount
      ) {
        return { ...item, feesPaymentStatus: newStatus };
      }
      return item;
    });
    this.currentMemberPaymentHistory.set(updatedData);
  }

  /**
   * Sorts payment history data by toDate in descending order.
   * @param data - Array of payment history records to sort
   * @returns Sorted array with most recent dates first
   */
  private sortPaymentHistoryByToDate(
    data: MemberPaymentHistoryDTO[]
  ): MemberPaymentHistoryDTO[] {
    return [...data].sort(
      (a, b) => new Date(b.toDate).getTime() - new Date(a.toDate).getTime()
    );
  }

  /**
   * Restores the previously saved scroll position to maintain user context.
   */
  private restoreScrollPosition(): void {
    requestAnimationFrame(() => {
      window.scrollTo({
        top: this._savedScrollPosition,
        behavior: 'instant',
      });
    });
  }

  /**
   * Loads payment history for the current member email.
   */
  private loadPaymentHistory(): void {
    const email = this.memberEmail();
    if (!email) return;

    this.getPaymentHistoryDataForMember(email);
  }

  /**
   * Fetches payment history data from the API for a specific member and displays it in the dialog.
   * @param memberEmail - The email address of the member to fetch payment history for
   * @param isRefresh - Whether this is a refresh operation (dialog already open)
   */
  private getPaymentHistoryDataForMember(
    memberEmail: string,
    isRefresh: boolean = false
  ): void {
    if (!memberEmail || this.isLoading()) return;

    if (!isRefresh) {
      this._savedScrollPosition =
        window.scrollY || document.documentElement.scrollTop;
    }

    this.isLoading.set(true);
    this._loaderService.loadingOn();

    this._memberFeesApiService
      .GetPaymentHistoryDataForMemberAsync(memberEmail)
      .pipe(
        switchMap((response: ResponseDto) => {
          if (response?.isSuccess && response?.responseData) {
            this.currentMemberPaymentHistory.set(response.responseData);
            if (!isRefresh) {
              this.showDialog.set(true);
            }
            return of(response);
          } else {
            this._toasterService.showError(response?.responseData);
            return EMPTY;
          }
        }),
        finalize(() => {
          this.isLoading.set(false);
          this._loaderService.loadingOff();
        })
      )
      .subscribe({
        error: (error: Error) => {
          this._toasterService.showError(error?.message);
          console.error(error);
        },
      });
  }
}
