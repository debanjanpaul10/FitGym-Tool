export class UpdateMemberFeesDTO {
  public memberEmailAddress: string = '';
  public amount: number = 0.0;
  public fromDate: Date = new Date();
  public toDate: Date = new Date();
}
