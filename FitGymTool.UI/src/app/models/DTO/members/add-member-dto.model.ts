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
