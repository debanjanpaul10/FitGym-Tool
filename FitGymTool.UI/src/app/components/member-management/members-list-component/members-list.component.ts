import { CommonModule } from '@angular/common';
import { Component, inject, Input, ViewChild } from '@angular/core';
import { Table, TableModule } from 'primeng/table';
import { DatePickerModule } from 'primeng/datepicker';
import { SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { FilterService, SortEvent } from 'primeng/api';

import { MemberDetailsDto } from '@models/DTO/members/memberdetails-dto.model';
import { Column } from '@models/interfaces/column.interface';
import { MembershipStatusMappingDto } from '@models/DTO/Mapping/membership-status-mapping-dto.model';

/**
 * Component responsible for displaying and filtering a list of gym members.
 * Provides sorting and filtering  capabilities ith a responsive table interface.
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
  templateUrl: './members-list.component.html',
  styleUrl: './members-list.component.scss',
})
export class MembersListComponent {
  @Input() membersData: MemberDetailsDto[] = [];
  @Input() membershipStatusOptions: MembershipStatusMappingDto[] = [];
  @ViewChild('dt') dt!: Table;

  protected columnHeaders!: Column[];
  protected selectedJoinDate: Date | null = null;
  protected selectedMembershipStatus: string | null = null;
  protected sortedMembersData!: MemberDetailsDto[];

  private isSorted: boolean | null = null;
  private currentSortField: string = 'memberId';

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
    // Default sort by memberId ascending
    this.sortedMembersData = [...this.membersData].sort(
      (a, b) => a.memberId - b.memberId
    );
    this.currentSortField = 'memberId';
  }

  /**
   * Handles join date filter changes by applying a custom date filter to the table.
   * Filters the table data to show only members who joined on the selected date.
   * @param event - The date selection event from the date picker
   * @param table - Reference to the PrimeNG table component
   */
  protected onJoinDateChange(event: any, table: any) {
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
  protected onMembershipStatusChange(event: any, table: any) {
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
  protected clearFilters(table: any) {
    this.selectedJoinDate = null;
    this.selectedMembershipStatus = null;
    table.filter('', 'memberJoinDate', 'custom');
    table.filter('', 'membershipStatus', 'equals');
  }

  /**
   * Handles custom sort cycling for the table: ascending -> descending -> reset (default order).
   * Updates the sort state and triggers sorting or reset as needed.
   * @param event - The sort event from the PrimeNG table containing field and order information
   */
  protected customSort(event: SortEvent) {
    const sortField = event.field ?? 'memberId';
    this.handleSorting(sortField);
  }

  // #region PRIVATE METHODS

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

  /**
   * Sorts the table data in place based on the provided sort event's field and order.
   * Handles nulls, strings, and numbers for robust sorting.
   * @param event - The sort event containing the field to sort by and the sort order
   */
  private sortTableData(event: any): void {
    const field: string = event.field ?? 'memberId';
    this.sortedMembersData.sort((data1: any, data2: any) => {
      let value1 = data1[field];
      let value2 = data2[field];
      let result = null;
      if (value1 == null && value2 != null) result = -1;
      else if (value1 != null && value2 == null) result = 1;
      else if (value1 == null && value2 == null) result = 0;
      else if (typeof value1 === 'string' && typeof value2 === 'string')
        result = value1.localeCompare(value2);
      else result = value1 < value2 ? -1 : value1 > value2 ? 1 : 0;
      return event.order * result;
    });
  }

  /**
   * Handles the sorting logic for the members table based on the provided field.
   * Cycles through ascending, descending, and reset (default memberId sort) states for the selected column.
   * If a new column is selected, sorting starts in ascending order for that column.
   * If the same column is clicked repeatedly, toggles between ascending, descending, and reset states.
   * On reset, the table is sorted by memberId in ascending order.
   *
   * @param sortField - The field name of the column to sort by.
   */
  private handleSorting(sortField: string): void {
    if (sortField !== this.currentSortField) {
      this.currentSortField = sortField;
      this.isSorted = true;
      this.sortTableData({
        field: sortField,
        order: 1,
        data: this.sortedMembersData,
      });
    } else if (this.isSorted == null || this.isSorted === undefined) {
      this.isSorted = true;
      this.sortTableData({
        field: sortField,
        order: 1,
        data: this.sortedMembersData,
      });
    } else if (this.isSorted == true) {
      this.isSorted = false;
      this.sortTableData({
        field: sortField,
        order: -1,
        data: this.sortedMembersData,
      });
    } else if (this.isSorted == false) {
      this.isSorted = null;
      // Reset to default sort (memberId asc)
      this.currentSortField = 'memberId';
      this.sortedMembersData = [...this.membersData].sort(
        (a, b) => a.memberId - b.memberId
      );
      this.dt.reset();
    }
  }

  // #endregion
}
