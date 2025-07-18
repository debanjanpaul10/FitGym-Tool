/**
 * Data transfer object for updating member membership status.
 * Contains member identification and new membership status information
 * used for changing member status (active, terminated, expired, etc.).
 */
export class UpdateMembershipStatusDto {
  public memberId: number = 0;
  public membershipStatusId: number = 0;
  public memberEmailAddress: string = '';
}
