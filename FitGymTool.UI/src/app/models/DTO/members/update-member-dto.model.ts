export class UpdateMemberDto {
  public memberId: number = 0;
  public memberName: string = '';
  public memberPhoneNumber: string = '';
  public memberAddress: string = '';
  public memberDateOfBirth: Date = new Date();
  public memberGender: string = '';
}
