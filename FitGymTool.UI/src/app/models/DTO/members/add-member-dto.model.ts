/**
 * Data transfer object for adding new gym members.
 * Contains all required information to create a new member record
 * including personal details, contact information, and membership data.
 */
export class AddMemberDto {
  public memberName: string = '';
  public memberEmail: string | null = null;
  public memberPhoneNumber: string = '';
  public memberAddress: string = '';
  public memberDateOfBirth: Date = new Date();
  public memberGender: string = '';
  public memberJoinDate: Date = new Date();
  public membershipStatus: string = '';
}
