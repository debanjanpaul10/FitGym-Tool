import {
  Component,
  effect,
  EventEmitter,
  inject,
  Input,
  OnDestroy,
  Output,
  signal,
  ViewChild,
  WritableSignal,
  ElementRef,
} from '@angular/core';
import { Table, TableModule } from 'primeng/table';
import { Dialog } from 'primeng/dialog';
import { SelectModule } from 'primeng/select';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { DatePickerModule } from 'primeng/datepicker';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

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
import { UpdateMemberDto } from '@models/DTO/members/update-member-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';

/**
 * Component for editing member details in a tabular format with validation and dirty checking.
 * Provides functionality to update member information with real-time validation and change tracking.
 */
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
export class EditMemberComponent implements OnDestroy {
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

  private readonly destroy$ = new Subject<void>();
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

  /**
   * Lifecycle hook that cleans up subscriptions to prevent memory leaks.
   * Completes the destroy subject to trigger unsubscription from all observables.
   */
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Handles the update of member details by calling the API service.
   * Validates member data, shows loading state, and handles success/error responses.
   * @param member - The member details to be updated
   */
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
      .pipe(takeUntil(this.destroy$))
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

  /**
   * Handles the cancel action by hiding the edit dialog.
   */
  protected onCancel(): void {
    this.visible.set(false);
  }

  /**
   * Determines if a member row has been modified from its original state.
   * Compares current member data with the stored original data.
   * @param member - The member data to check for changes
   * @returns True if the member data has been modified, false otherwise
   */
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

  /**
   * Validates all fields of a member record to ensure data integrity.
   * Checks name, email, phone, address, gender, and date fields.
   * @param member - The member data to validate
   * @returns True if all member fields are valid, false otherwise
   */
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

  /**
   * Validates a member's name field.
   * @param name - The name string to validate
   * @returns True if name is between 2-100 characters, false otherwise
   */
  protected isValidName(name: string): boolean {
    return name !== '' && name.trim().length >= 2 && name.trim().length <= 100;
  }

  /**
   * Validates a member's email address using regex pattern.
   * @param email - The email string to validate
   * @returns True if email matches valid email format, false otherwise
   */
  protected isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return email !== '' && emailRegex.test(email.trim());
  }

  /**
   * Validates a member's phone number format.
   * @param phone - The phone number string to validate
   * @returns True if phone number contains exactly 10 digits, false otherwise
   */
  protected isValidPhoneNumber(phone: string): boolean {
    const phoneRegex = /^\d{10}$/;
    return phone !== '' && phoneRegex.test(phone.replace(/\s/g, ''));
  }

  /**
   * Validates a member's address field.
   * @param address - The address string to validate
   * @returns True if address is between 5-500 characters, false otherwise
   */
  protected isValidAddress(address: string): boolean {
    return (
      address !== '' &&
      address.trim().length >= 5 &&
      address.trim().length <= 500
    );
  }

  /**
   * Validates a member's gender selection.
   * @param gender - The gender string to validate
   * @returns True if gender is one of the allowed values (Male, Female, Other), false otherwise
   */
  protected isValidGender(gender: string): boolean {
    return gender !== '' && ['Male', 'Female', 'Other'].includes(gender);
  }

  /**
   * Validates a date object to ensure it's a valid Date instance.
   * @param date - The Date object to validate
   * @returns True if date is a valid Date object, false otherwise
   */
  protected isValidDate(date: Date): boolean {
    return date && date instanceof Date && !isNaN(date.getTime());
  }

  /**
   * Determines if a member row can be updated based on dirty state and validation.
   * @param member - The member data to check
   * @returns True if the row has changes and all data is valid, false otherwise
   */
  protected canUpdateRow(member: MemberDetailsDto): boolean {
    return this.isRowDirty(member) && this.isRowValid(member);
  }

  /**
   * Gets the validation error message for a specific field of a member.
   * @param member - The member data containing the field to validate
   * @param field - The field name to get error message for
   * @returns Error message string if field is invalid, empty string if valid
   */
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

  /**
   * Checks if a specific field of a member has validation errors.
   * @param member - The member data containing the field to check
   * @param field - The field name to check for errors
   * @returns True if the field has validation errors, false otherwise
   */
  protected hasFieldError(member: MemberDetailsDto, field: string): boolean {
    return this.getFieldError(member, field) !== '';
  }

  /**
   * Compares two Date objects to determine if they are different.
   * @param date1 - First date to compare
   * @param date2 - Second date to compare
   * @returns True if dates are different, false if they are the same
   */
  private compareDates(date1: Date, date2: Date): boolean {
    if (!date1 && !date2) return false;
    if (!date1 || !date2) return true;
    return date1.getTime() !== date2.getTime();
  }
}
