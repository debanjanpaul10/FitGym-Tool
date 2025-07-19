/**
 * Data transfer object for complete member details information.
 * Contains comprehensive member data including identification, personal details,
 * contact information, and membership status used for member management operations.
 */
export class MemberDetailsDto {
  public memberId: number = 0;
  public memberName: string = '';
  public memberEmail: string = '';
  public memberPhoneNumber: string = '';
  public memberAddress: string = '';
  public memberDateOfBirth: Date = new Date();
  public memberGender: string = '';
  public memberJoinDate: Date = new Date();
  public membershipStatus: string = '';
  public memberGuid: string = '';
}
