import {
  Component,
  inject,
  OnInit,
  signal,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';

import { FeesManagementService } from '@core/services/fees-management-service.service';
import { CurrentMembersFeesStatusDTO } from '@models/DTO/current-members-fees-status-dto.model';
import { Column } from '@models/interfaces/column.interface';
import { FeesManagementConstants } from '@shared/application.constants';

@Component({
  selector: 'app-member-fees-payment-list',
  imports: [CommonModule, TableModule],
  templateUrl: './member-fees-payment-list.component.html',
  styleUrl: './member-fees-payment-list.component.scss',
})
export class MemberFeesPaymentListComponent implements OnInit {
  @ViewChild('feesPaymentListTable') feesPaymentListTable!: Table;

  protected currentMemberFeesData: WritableSignal<
    CurrentMembersFeesStatusDTO[]
  > = signal([]);
  protected columnHeaders!: Column[];
  protected memberFeesConstants =
    FeesManagementConstants.MemberFeesPaymentListConstants;

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
      { field: 'feesAmountDue', header: 'Due Amount (₹)' },
      { field: 'feesPaymentStatus', header: 'Fee Payment Status' },
      { field: 'lastPaymentDate', header: 'Last Payment Date' },
    ];
  }

  ngOnInit(): void {
    this._feesManagementService.currentMemberFees.subscribe(
      (data: CurrentMembersFeesStatusDTO[]) => {
        if (data && data.length > 0) {
          this.currentMemberFeesData.set(data);
          console.log(this.currentMemberFeesData());
        }
      }
    );
  }
}
