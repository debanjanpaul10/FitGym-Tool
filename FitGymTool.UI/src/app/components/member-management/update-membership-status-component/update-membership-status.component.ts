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
import { MemberDetailsWithStatusId } from '@models/interfaces/memberdetailsstatusid.interface';

/**
 * Component for updating membership status of members in a tabular format.
 * Provides functionality to change member status with validation and API integration.
 */
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
  protected sortedMembersData: MemberDetailsWithStatusId[] = [];
  protected columnHeaders: Column[] = [];
  protected originalStatusMap: Map<number, number | null> = new Map();
  protected updatedMembersSet: Set<number> = new Set();

  private readonly _dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly _loaderService: LoaderService = inject(LoaderService);
  private readonly _membersApiService: MembersApiService =
    inject(MembersApiService);
  private readonly _toasterService: ToasterService = inject(ToasterService);

  constructor() {
    this.visible = this._dialogPopupService.isUpdateMembershipDialogOpen;
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
        // Clear the updated members set when dialog opens
        this.updatedMembersSet.clear();
        this.sortedMembersData = [...this.membersData]
          .map((member) => {
            const statusObj = this.membershipStatusOptions.find(
              (opt) => opt.statusName === member.membershipStatus
            );
            const statusId = statusObj ? statusObj.id : 0;
            // Store original status for comparison
            this.originalStatusMap.set(member.memberId, statusId);
            return {
              ...member,
              membershipStatusId: statusId,
            };
          })
          .sort((m1, m2) => m1.memberId - m2.memberId);
      }
    });
  }

  /**
   * Handles the update of membership status by calling the API service.
   * Validates status selection, shows loading state, and handles success/error responses.
   * @param member - The member whose status is being updated
   * @param newStatusId - The new membership status ID to be assigned
   */
  protected onMembershipStatusChangesUpdate(
    member: MemberDetailsWithStatusId,
    newStatusId: number | null
  ): void {
    if (!newStatusId || newStatusId === 0) {
      this._toasterService.showError('Please enter a valid status');
      return;
    }

    this._loaderService.loadingOn();
    const updateMembershipData: UpdateMembershipStatusDto = {
      memberEmailAddress: member.memberEmail,
      memberId: member.memberId,
      membershipStatusId: newStatusId,
    };
    this._membersApiService
      .UpdateMembershipStatusAsync(updateMembershipData)
      .subscribe({
        next: (response: ResponseDto) => {
          if (response?.isSuccess && response?.responseData) {
            this._toasterService.showSuccess(
              ToasterSuccessMessages.MemberManagement
                .MembershipStatusUpdatedSuccess
            );
            // Mark this member as successfully updated
            this.updatedMembersSet.add(member.memberId);
            // Update the original status to the new status to prevent further updates
            this.originalStatusMap.set(member.memberId, newStatusId);
            this.membershipStatusUpdate.emit();
          } else {
            this._toasterService.showError(response?.responseData);
          }
        },
        error: (error: Error) => {
          this._loaderService.loadingOff();
          console.error(error.message);
          this._toasterService.showError(error.message);
        },
        complete: () => {
          this._loaderService.loadingOff();
        },
      });
  }

  /**
   * Checks if the membership status has been changed for a specific member
   * @param member - The member to check for changes
   * @returns true if the status has been changed, false otherwise
   */
  protected hasStatusChanged(member: MemberDetailsWithStatusId): boolean {
    const originalStatus = this.originalStatusMap.get(member.memberId);
    return originalStatus !== member.membershipStatusId;
  }

  /**
   * Checks if the update button should be enabled for a specific member
   * @param member - The member to check
   * @returns true if the button should be enabled, false otherwise
   */
  protected isUpdateButtonEnabled(member: MemberDetailsWithStatusId): boolean {
    // Button should be enabled only if:
    // 1. The status has changed from the original
    // 2. The member hasn't been successfully updated yet
    return (
      this.hasStatusChanged(member) &&
      !this.updatedMembersSet.has(member.memberId)
    );
  }

  /**
   * Handles changes to the membership status dropdown
   * If the member was previously updated successfully, this will re-enable the update button
   * @param member - The member whose status selection has changed
   */
  protected onStatusSelectionChange(member: MemberDetailsWithStatusId): void {
    // If this member was previously updated and is now being changed again
    if (
      this.updatedMembersSet.has(member.memberId) &&
      this.hasStatusChanged(member)
    ) {
      // Remove from the updated set to re-enable the update button
      this.updatedMembersSet.delete(member.memberId);
    }
  }

  /**
   * Handles the cancel action by hiding the membership status dialog.
   */
  protected onCancel(): void {
    this.visible.set(false);
  }
}
