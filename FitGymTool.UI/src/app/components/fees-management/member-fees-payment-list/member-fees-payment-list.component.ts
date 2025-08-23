import {
  Component,
  inject,
  OnInit,
  output,
  signal,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { Button } from 'primeng/button';
import { Ripple } from 'primeng/ripple';

import { FeesManagementService } from '@core/services/fees-management-service.service';
import { CurrentMembersFeesStatusDTO } from '@models/DTO/current-members-fees-status-dto.model';
import { Column } from '@models/interfaces/column.interface';
import { FeesManagementConstants } from '@shared/application.constants';
import { MemberFeesPaymentHistoryComponent } from '../member-fees-payment-history/member-fees-payment-history.component';

@Component({
  selector: 'app-member-fees-payment-list',
  imports: [
    CommonModule,
    TableModule,
    MemberFeesPaymentHistoryComponent,
    Button,
    Ripple,
  ],
  templateUrl: './member-fees-payment-list.component.html',
  styleUrl: './member-fees-payment-list.component.scss',
})
export class MemberFeesPaymentListComponent implements OnInit {
  @ViewChild('feesPaymentListTable') feesPaymentListTable!: Table;
  readonly onPaymentUpdated = output<void>();

  protected currentMemberFeesData: WritableSignal<
    CurrentMembersFeesStatusDTO[]
  > = signal([]);
  protected columnHeaders!: Column[];
  protected memberFeesConstants =
    FeesManagementConstants.MemberFeesPaymentListConstants;
  protected selectedMember: string = '';

  private readonly _feesManagementService: FeesManagementService = inject(
    FeesManagementService
  );

  constructor() {
    this.columnHeaders = [
      { field: 'memberId', header: 'Member Id' },
      { field: 'memberName', header: 'Name' },
      { field: 'memberEmail', header: 'Email' },
      { field: 'memberStatus', header: 'Membership Status' },
      { field: 'dueDate', header: 'Payment Due Date' },
      { field: 'dueAmount', header: 'Due Amount (₹)' },
      { field: 'feesPaymentStatus', header: 'Fee Payment Status' },
      { field: '', header: '' },
    ];
  }

  ngOnInit(): void {
    this._feesManagementService.currentMemberFees.subscribe(
      (data: CurrentMembersFeesStatusDTO[]) => {
        if (data && data.length > 0) {
          this.currentMemberFeesData.set(data);
        }
      }
    );
  }

  protected handleMemberClick(event: any): void {
    this.selectedMember = '';
    setTimeout(() => {
      this.selectedMember = event;
    }, 0);
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

  protected onPaymentHistoryUpdated(): void {
    this.onPaymentUpdated.emit();
  }
}
