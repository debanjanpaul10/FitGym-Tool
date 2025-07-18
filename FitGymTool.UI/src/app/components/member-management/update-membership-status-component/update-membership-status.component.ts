import {
  Component,
  effect,
  EventEmitter,
  inject,
  Input,
  Output,
  signal,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { Dialog } from 'primeng/dialog';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { IftaLabel } from 'primeng/iftalabel';

import { MembershipStatusMappingDto } from '@models/DTO/Mapping/membership-status-mapping-dto.model';
import { DialogPopupService } from '@core/services/dialog-popup.service';
import { MemberDetailsDto } from '@models/DTO/members/memberdetails-dto.model';
import { Column } from '@models/interfaces/column.interface';
import {
  MemberManagementConstants,
  ToasterSuccessMessages,
} from '@shared/application.constants';
import { LoaderService } from '@core/services/loader.service';
import { MembersApiService } from '@services/members-api.service';
import { UpdateMembershipStatusDto } from '@models/DTO/members/update-membership-status-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ToasterService } from '@core/services/toaster.service';

@Component({
  selector: 'app-update-membership-status-component',
  imports: [
    Dialog,
    TableModule,
    SelectModule,
    CommonModule,
    ButtonModule,
    IftaLabel,
    FormsModule,
  ],
  templateUrl: './update-membership-status.component.html',
  styleUrl: './update-membership-status.component.scss',
})
export class UpdateMembershipStatusComponent {
  @Input() membersData: MemberDetailsDto[] = [];
  @Input() membershipStatusOptions: MembershipStatusMappingDto[] = [];
  @Output() membershipStatusUpdate: EventEmitter<void> =
    new EventEmitter<void>();
  @ViewChild('membershipStatusTable') membershipStatusTable!: Table;

  protected membershipStatusConstants =
    MemberManagementConstants.UpdateMembershipStatusConstants;
  protected visible: WritableSignal<boolean> = signal(false);
  protected sortedMembersData: MemberDetailsDto[] = [];
  protected columnHeaders: Column[] = [];

  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly membersApiService: MembersApiService =
    inject(MembersApiService);
  private readonly toasterService: ToasterService = inject(ToasterService);

  constructor() {
    this.visible = this.dialogPopupService.isUpdateMembershipDialogOpen;
    this.columnHeaders = [
      { field: 'memberId', header: 'Member Id' },
      { field: 'memberName', header: 'Name' },
      { field: 'memberEmail', header: 'Email' },
      { field: 'memberJoinDate', header: 'Join Date' },
      { field: 'membershipStatus', header: 'Membership Status' },
      { field: 'actions', header: '' },
    ];

    effect(() => {
      if (this.visible()) {
        this.sortedMembersData = [...this.membersData]
          .map((member) => {
            const statusObj = this.membershipStatusOptions.find(
              (opt) => opt.statusName === member.membershipStatus
            );
            return {
              ...member,
              membershipStatusId: statusObj ? statusObj.id : null,
            };
          })
          .sort((m1, m2) => m1.memberId - m2.memberId);
      }
    });
  }

  protected onMembershipStatusChangesUpdate(
    member: MemberDetailsDto,
    newStatusId: number
  ): void {
    if (!newStatusId || newStatusId === 0) {
      this.toasterService.showError('Please enter a valid status');
      return;
    }

    this.loaderService.loadingOn();
    const updateMembershipData: UpdateMembershipStatusDto = {
      memberEmailAddress: member.memberEmail,
      memberId: member.memberId,
      membershipStatusId: newStatusId,
    };
    this.membersApiService
      .UpdateMembershipStatusAsync(updateMembershipData)
      .subscribe({
        next: (response: ResponseDto) => {
          if (response?.isSuccess && response?.responseData) {
            this.toasterService.showSuccess(
              ToasterSuccessMessages.MemberManagement
                .MembershipStatusUpdatedSuccess
            );
            this.membershipStatusUpdate.emit();
          } else {
            this.toasterService.showError(response?.responseData);
          }
        },
        error: (error: Error) => {
          this.loaderService.loadingOff();
          console.error(error.message);
          this.toasterService.showError(error.message);
        },
        complete: () => {
          this.loaderService.loadingOff();
        },
      });
  }

  protected onCancel(): void {
    this.visible.set(false);
  }
}
