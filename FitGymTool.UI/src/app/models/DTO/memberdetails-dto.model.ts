export class MemberDetailsDto {
  public memberId: number;

  public memberName: string;

  public memberEmail: string;

  public memberPhoneNumber: string;

  public memberAddress: string;

  public memberDateOfBirth: Date;

  public memberGender: string;

  public memberJoinDate: Date;

  public membershipStatus: number;

  public memberGuid: string;

  constructor(
    MemberId: number = 0,
    MemberName: string = '',
    MemberEmail: string = '',
    MemberPhoneNumber: string = '',
    MemberAddress: string = '',
    MemberDateOfBirth: Date = new Date(),
    MemberGender: string = '',
    MemberJoinDate: Date = new Date(),
    MembershipStatus: number = 0,
    MemberGuid: string = ''
  ) {
    this.memberId = MemberId;
    this.memberName = MemberName;
    this.memberEmail = MemberEmail;
    this.memberPhoneNumber = MemberPhoneNumber;
    this.memberAddress = MemberAddress;
    this.memberDateOfBirth = MemberDateOfBirth;
    this.memberGender = MemberGender;
    this.memberJoinDate = MemberJoinDate;
    this.membershipStatus = MembershipStatus;
    this.memberGuid = MemberGuid;
  }
}
