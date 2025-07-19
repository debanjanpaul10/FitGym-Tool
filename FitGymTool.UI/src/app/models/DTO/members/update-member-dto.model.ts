/**
 * Data transfer object for updating existing member information.
 * Contains member identification and updatable fields for modifying
 * member personal details and contact information.
 */
export class UpdateMemberDto {
  public memberId: number = 0;
  public memberName: string = '';
  public memberPhoneNumber: string = '';
  public memberAddress: string = '';
  public memberDateOfBirth: Date = new Date();
  public memberJoinDate: Date = new Date();
  public memberGender: string = '';
}
