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
export class EditMemberComponent {
  @Input() membersData: MemberDetailsDto[] = [];
  @Input() membershipStatusOptions: MembershipStatusMappingDto[] = [];
  @Output() memberDetailsUpdate: EventEmitter<void> = new EventEmitter<void>();
  @ViewChild('memberDetailsUpdateTable') memberDetailsUpdateTable!: Table;
  @ViewChild('tableContainer') tableContainer!: ElementRef;

  protected memberDetailsUpdateConstants =
    MemberManagementConstants.UpdateMemberDetailsConstants;
  protected visible: WritableSignal<boolean> = signal(false);
  protected sortedMembersData: MemberDetailsDto[] = [];
  protected originalMembersData: Map<number, MemberDetailsDto> = new Map();
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

        // Store original data for dirty checking
        this.originalMembersData.clear();
        this.sortedMembersData.forEach((member) => {
          this.originalMembersData.set(member.memberId, { ...member });
        });
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
      memberJoinDate: member.memberJoinDate,
    };

    this.membersApiService
      .UpdateMemberDetailsAsync(updateMemberDetailsData)
      .subscribe({
        next: (response: ResponseDto) => {
          if (response?.isSuccess && response?.responseData) {
            this.toasterService.showSuccess(
              ToasterSuccessMessages.MemberManagement.UpdateMemberSuccess
            );
            // Update the original data to reflect the saved state, which will disable the button
            this.originalMembersData.set(member.memberId, { ...member });
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

  protected isRowDirty(member: MemberDetailsDto): boolean {
    const original = this.originalMembersData.get(member.memberId);
    if (!original) return false;

    return (
      original.memberName !== member.memberName ||
      original.memberEmail !== member.memberEmail ||
      original.memberPhoneNumber !== member.memberPhoneNumber ||
      original.memberAddress !== member.memberAddress ||
      original.memberGender !== member.memberGender ||
      this.compareDates(original.memberDateOfBirth, member.memberDateOfBirth) ||
      this.compareDates(original.memberJoinDate, member.memberJoinDate)
    );
  }

  protected isRowValid(member: MemberDetailsDto): boolean {
    return (
      this.isValidName(member.memberName) &&
      this.isValidEmail(member.memberEmail) &&
      this.isValidPhoneNumber(member.memberPhoneNumber) &&
      this.isValidAddress(member.memberAddress) &&
      this.isValidGender(member.memberGender) &&
      this.isValidDate(member.memberDateOfBirth) &&
      this.isValidDate(member.memberJoinDate)
    );
  }

  protected isValidName(name: string): boolean {
    return name !== '' && name.trim().length >= 2 && name.trim().length <= 100;
  }

  protected isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return email !== '' && emailRegex.test(email.trim());
  }

  protected isValidPhoneNumber(phone: string): boolean {
    const phoneRegex = /^\d{10}$/;
    return phone !== '' && phoneRegex.test(phone.replace(/\s/g, ''));
  }

  protected isValidAddress(address: string): boolean {
    return (
      address !== '' &&
      address.trim().length >= 5 &&
      address.trim().length <= 500
    );
  }

  protected isValidGender(gender: string): boolean {
    return gender !== '' && ['Male', 'Female', 'Other'].includes(gender);
  }

  protected isValidDate(date: Date): boolean {
    return date && date instanceof Date && !isNaN(date.getTime());
  }

  protected canUpdateRow(member: MemberDetailsDto): boolean {
    return this.isRowDirty(member) && this.isRowValid(member);
  }

  protected getFieldError(member: MemberDetailsDto, field: string): string {
    switch (field) {
      case 'memberName':
        if (!this.isValidName(member.memberName)) {
          return 'Name must be between 2-100 characters';
        }
        break;
      case 'memberEmail':
        if (!this.isValidEmail(member.memberEmail)) {
          return 'Please enter a valid email address';
        }
        break;
      case 'memberPhoneNumber':
        if (!this.isValidPhoneNumber(member.memberPhoneNumber)) {
          return 'Phone number must be exactly 10 digits';
        }
        break;
      case 'memberAddress':
        if (!this.isValidAddress(member.memberAddress)) {
          return 'Address must be between 5-500 characters';
        }
        break;
      case 'memberGender':
        if (!this.isValidGender(member.memberGender)) {
          return 'Please select a valid gender';
        }
        break;
      case 'memberDateOfBirth':
      case 'memberJoinDate':
        if (
          !this.isValidDate(member[field as keyof MemberDetailsDto] as Date)
        ) {
          return 'Please select a valid date';
        }
        break;
    }
    return '';
  }

  protected hasFieldError(member: MemberDetailsDto, field: string): boolean {
    return this.getFieldError(member, field) !== '';
  }

  private compareDates(date1: Date, date2: Date): boolean {
    if (!date1 && !date2) return false;
    if (!date1 || !date2) return true;
    return date1.getTime() !== date2.getTime();
  }
}
