import { CommonModule } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { TableModule } from 'primeng/table';
import { DatePickerModule } from 'primeng/datepicker';
import { SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { FilterService } from 'primeng/api';

import { MemberDetailsDto } from '@models/DTO/memberdetails-dto.model';
import { Column } from '@models/interfaces/column.interface';

/**
 * Component responsible for displaying and filtering a list of gym members.
 * Provides date-based and membership status filtering capabilities with a responsive table interface.
 */
@Component({
  selector: 'app-members-list-component',
  imports: [
    TableModule,
    CommonModule,
    DatePickerModule,
    SelectModule,
    ButtonModule,
    FormsModule,
  ],
  templateUrl: './members-list-component.html',
  styleUrl: './members-list-component.scss',
})
export class MembersListComponent {
  @Input() public membersData: MemberDetailsDto[] | null = null;
  public columnHeaders!: Column[];
  public membershipStatusOptions = [
    { label: 'Active', value: 'Active' },
    { label: 'Expired', value: 'Expired' },
    { label: 'On Termination', value: 'On Termination' },
  ];
  public selectedJoinDate: Date | null = null;
  public selectedMembershipStatus: string | null = null;

  private readonly filterService: FilterService = inject(FilterService);

  constructor() {
    this.columnHeaders = [
      { field: 'memberId', header: 'Member Id' },
      { field: 'memberName', header: 'Name' },
      { field: 'memberEmail', header: 'Email' },
      { field: 'memberPhoneNumber', header: 'Phone Number' },
      { field: 'memberAddress', header: 'Address' },
      { field: 'memberDateOfBirth', header: 'Date of Birth' },
      { field: 'memberGender', header: 'Gender' },
      { field: 'memberJoinDate', header: 'Join Date' },
      { field: 'membershipStatus', header: 'Membership Status' },
    ];
  }

  ngOnInit() {
    this.filterService.register(
      'custom',
      (value: any, filter: any): boolean => {
        if (!filter) return true;
        if (!value) return false;
        // Convert both to yyyy-MM-dd for comparison
        const valueDate = this.formatDateToYMD(new Date(value));
        const filterDate = this.formatDateToYMD(new Date(filter));
        return valueDate === filterDate;
      }
    );
  }

  /**
   * Handles join date filter changes by applying a custom date filter to the table.
   * Filters the table data to show only members who joined on the selected date.
   * @param event - The date selection event from the date picker
   * @param table - Reference to the PrimeNG table component
   */
  public onJoinDateChange(event: any, table: any) {
    if (this.selectedJoinDate) {
      table.filter(this.selectedJoinDate, 'memberJoinDate', 'custom');
    } else {
      table.filter('', 'memberJoinDate', 'custom');
    }
  }

  /**
   * Handles membership status filter changes by applying an exact match filter to the table.
   * Filters the table data to show only members with the selected membership status.
   * @param event - The selection change event from the dropdown
   * @param table - Reference to the PrimeNG table component
   */
  public onMembershipStatusChange(event: any, table: any) {
    if (this.selectedMembershipStatus) {
      table.filter(this.selectedMembershipStatus, 'membershipStatus', 'equals');
    } else {
      table.filter('', 'membershipStatus', 'equals');
    }
  }

  /**
   * Clears all active filters and resets the filter controls to their default state.
   * Removes both date and membership status filters from the table.
   * @param table - Reference to the PrimeNG table component
   */
  public clearFilters(table: any) {
    this.selectedJoinDate = null;
    this.selectedMembershipStatus = null;
    table.filter('', 'memberJoinDate', 'custom');
    table.filter('', 'membershipStatus', 'equals');
  }

  /**
   * Converts a Date object to a standardized yyyy-MM-dd string format.
   * Ensures consistent date formatting for comparison operations in filters.
   * @param date - The Date object to format
   * @returns A string representation of the date in yyyy-MM-dd format
   */
  private formatDateToYMD(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
}
