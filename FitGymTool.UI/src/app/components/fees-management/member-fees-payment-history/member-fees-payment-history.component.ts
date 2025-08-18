import {
  Component,
  inject,
  Input,
  OnChanges,
  signal,
  SimpleChanges,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';

import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@core/services/toaster.service';
import { MemberPaymentHistoryDTO } from '@models/DTO/member-payment-history-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { MemberFeesApiService } from '@services/member-fees-api.service';
import { Column } from '@models/interfaces/column.interface';

@Component({
  selector: 'app-member-fees-payment-history',
  imports: [DialogModule, ButtonModule, TableModule, CommonModule],
  templateUrl: './member-fees-payment-history.component.html',
  styleUrl: './member-fees-payment-history.component.scss',
})
export class MemberFeesPaymentHistoryComponent implements OnChanges {
  @Input() memberEmail: string = '';
  @ViewChild('paymentHistoryTable') paymentHistoryTable!: Table;

  protected currentMemberPaymentHistory: WritableSignal<
    MemberPaymentHistoryDTO[]
  > = signal([]);
  protected showDialog: WritableSignal<boolean> = signal(false);
  protected columnHeaders: Column[] = [];

  private savedScrollPosition: number = 0;
  private readonly _memberFeesApiService: MemberFeesApiService =
    inject(MemberFeesApiService);
  private readonly _loaderService: LoaderService = inject(LoaderService);
  private readonly _toasterService: ToasterService = inject(ToasterService);

  constructor() {
    this.columnHeaders = [
      { field: 'memberId', header: 'Member ID' },
      { field: 'memberName', header: 'Member Name' },
      { field: 'memberEmail', header: 'Email Address' },
      { field: 'memberStatus', header: 'Membership Status' },
      { field: 'feesPaymentStatus', header: 'Payment Status' },
      { field: 'amount', header: 'Fees Amount ' },
      { field: 'fromDate', header: 'From Date' },
      { field: 'toDate', header: 'To Date' },
    ];
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['memberEmail'] && changes['memberEmail'].currentValue !== '') {
      this.getPaymentHistoryDataForMember(this.memberEmail);
    }
  }

  protected closeDialog(): void {
    this.showDialog.set(false);
  }

  protected onDialogShow(): void {
    // Restore scroll position when dialog is shown
    requestAnimationFrame(() => {
      window.scrollTo({
        top: this.savedScrollPosition,
        behavior: 'instant',
      });
    });
  }

  protected onDialogHide(): void {
    // Restore scroll position when dialog is hidden
    requestAnimationFrame(() => {
      window.scrollTo({
        top: this.savedScrollPosition,
        behavior: 'instant',
      });
    });
  }

  protected getPaymentStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'paid':
        return 'bg-success text-white';
      case 'pending':
        return 'bg-info text-white';
      case 'overdue':
        return 'bg-danger text-white';
      case 'due':
        return 'bg-warning text-dark';
      default:
        return 'bg-secondary text-white';
    }
  }

  private getPaymentHistoryDataForMember(memberEmail: string): void {
    // Save current scroll position before any operations
    this.savedScrollPosition =
      window.scrollY || document.documentElement.scrollTop;

    this._loaderService.loadingOn();

    this._memberFeesApiService
      .GetPaymentHistoryDataForMemberAsync(memberEmail)
      .subscribe({
        next: (response: ResponseDto) => {
          if (response?.isSuccess && response?.responseData) {
            this.currentMemberPaymentHistory.set(response.responseData);
            this.showDialog.set(true);
          } else {
            this._toasterService.showError(response?.responseData);
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
