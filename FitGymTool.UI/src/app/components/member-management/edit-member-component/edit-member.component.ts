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
  ElementRef,
  AfterViewInit,
} from '@angular/core';
import { Table, TableModule } from 'primeng/table';
import { Dialog } from 'primeng/dialog';

import { DialogPopupService } from '@core/services/dialog-popup.service';
import { LoaderService } from '@core/services/loader.service';
import { ToasterService } from '@core/services/toaster.service';
import { MembershipStatusMappingDto } from '@models/DTO/Mapping/membership-status-mapping-dto.model';
import { MemberDetailsDto } from '@models/DTO/members/memberdetails-dto.model';
import { Column } from '@models/interfaces/column.interface';
import { MembersApiService } from '@services/members-api.service';
import {
  MemberManagementConstants,
  ToasterSuccessMessages,
} from '@shared/application.constants';
import { SelectModule } from 'primeng/select';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';

import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { DatePickerModule } from 'primeng/datepicker';
import { UpdateMemberDto } from '@models/DTO/members/update-member-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';

@Component({
  selector: 'app-edit-member-component',
  imports: [
    Dialog,
    TableModule,
    SelectModule,
    CommonModule,
    ButtonModule,

    FormsModule,
    InputTextModule,
    TextareaModule,
    DatePickerModule,
  ],
  templateUrl: './edit-member.component.html',
  styleUrl: './edit-member.component.scss',
})
export class EditMemberComponent implements AfterViewInit {
  @Input() membersData: MemberDetailsDto[] = [];
  @Input() membershipStatusOptions: MembershipStatusMappingDto[] = [];
  @Output() memberDetailsUpdate: EventEmitter<void> = new EventEmitter<void>();
  @ViewChild('memberDetailsUpdateTable') memberDetailsUpdateTable!: Table;
  @ViewChild('tableContainer') tableContainer!: ElementRef;

  protected memberDetailsUpdateConstants =
    MemberManagementConstants.UpdateMemberDetailsConstants;
  protected visible: WritableSignal<boolean> = signal(false);
  protected sortedMembersData: MemberDetailsDto[] = [];
  protected columnHeaders: Column[] = [];
  protected genderOptions = [
    { label: 'Male', value: 'Male' },
    { label: 'Female', value: 'Female' },
    { label: 'Other', value: 'Other' },
  ];
  protected showScrollIndicator = false;
  protected scrollDirection: 'left' | 'right' = 'right';

  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly membersApiService: MembersApiService =
    inject(MembersApiService);
  private readonly toasterService: ToasterService = inject(ToasterService);

  constructor() {
    this.visible = this.dialogPopupService.isUpdateMemberDetailsDialogOpen;
    this.columnHeaders = [
      { field: 'memberId', header: 'Member Id' },
      { field: 'memberName', header: 'Name' },
      { field: 'memberEmail', header: 'Email' },
      { field: 'memberPhoneNumber', header: 'Phone Number' },
      { field: 'memberAddress', header: 'Address' },
      { field: 'memberDateOfBirth', header: 'Date of Birth' },
      { field: 'memberGender', header: 'Gender' },
      { field: 'memberJoinDate', header: 'Join Date' },
      { field: 'actions', header: '' },
    ];

    effect(() => {
      if (this.visible()) {
        this.sortedMembersData = [...this.membersData]
          .map((member) => ({
            ...member,
            memberDateOfBirth: member.memberDateOfBirth
              ? new Date(member.memberDateOfBirth)
              : new Date(),
            memberJoinDate: member.memberJoinDate
              ? new Date(member.memberJoinDate)
              : new Date(),
          }))
          .sort((m1, m2) => m1.memberId - m2.memberId);

        // Check scroll indicator after data is loaded
        setTimeout(() => this.checkScrollIndicator(), 500);
      }
    });
  }
  protected onMemberDetailsChangeUpdate(member: MemberDetailsDto): void {
    if (!member) {
      this.toasterService.showError('Enter the details, they cannot be null');
      return;
    }

    this.loaderService.loadingOn();
    const updateMemberDetailsData: UpdateMemberDto = {
      memberAddress: member.memberAddress,
      memberDateOfBirth: member.memberDateOfBirth,
      memberGender: member.memberGender,
      memberId: member.memberId,
      memberName: member.memberName,
      memberPhoneNumber: member.memberPhoneNumber,
    };

    this.membersApiService
      .UpdateMemberDetailsAsync(updateMemberDetailsData)
      .subscribe({
        next: (response: ResponseDto) => {
          if (response?.isSuccess && response?.responseData) {
            this.toasterService.showSuccess(
              ToasterSuccessMessages.MemberManagement.UpdateMemberSuccess
            );
            this.memberDetailsUpdate.emit();
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

  ngAfterViewInit(): void {
    // Check scroll indicator when view is initialized
    setTimeout(() => this.checkScrollIndicator(), 100);
  }

  protected onTableScroll(event: Event): void {
    this.checkScrollIndicator();
  }

  private checkScrollIndicator(): void {
    if (!this.tableContainer) {
      console.log('No table container found');
      return;
    }

    const element = this.tableContainer.nativeElement;
    const scrollLeft = element.scrollLeft;
    const scrollWidth = element.scrollWidth;
    const clientWidth = element.clientWidth;
    const maxScrollLeft = scrollWidth - clientWidth;

    console.log('Scroll check:', {
      scrollLeft,
      scrollWidth,
      clientWidth,
      maxScrollLeft,
    });

    // Show indicator if there's content to scroll
    this.showScrollIndicator = scrollWidth > clientWidth;

    // For testing, let's always show the indicator
    this.showScrollIndicator = true;

    if (this.showScrollIndicator) {
      // If at the beginning, show right arrow
      if (scrollLeft <= 10) {
        this.scrollDirection = 'right';
      }
      // If at the end, show left arrow
      else if (scrollLeft >= maxScrollLeft - 10) {
        this.scrollDirection = 'left';
      }
      // If in the middle, show right arrow (default)
      else {
        this.scrollDirection = 'right';
      }
    }

    console.log('Scroll indicator state:', {
      showScrollIndicator: this.showScrollIndicator,
      scrollDirection: this.scrollDirection,
    });
  }
}
