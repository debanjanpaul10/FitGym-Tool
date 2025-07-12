import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MemberDetailsDto } from '@models/DTO/memberdetails-dto.model';
import { TableModule } from 'primeng/table';

interface Column {
  field: string;
  header: string;
}

@Component({
  selector: 'app-members-list-component',
  imports: [TableModule, CommonModule],
  templateUrl: './members-list-component.html',
  styleUrl: './members-list-component.scss',
})
export class MembersListComponent {
  @Input() public membersData: MemberDetailsDto[] | null = null;
  public columnHeaders!: Column[];

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
}
