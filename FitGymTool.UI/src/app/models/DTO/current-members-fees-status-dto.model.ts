export class CurrentMembersFeesStatusDTO {
  public memberId: number = 0;
  public memberName: string = '';
  public memberEmail: string = '';
  public memberStatus: string = '';
  public feesAmountDue: number = 0.0;
  public dueDate: Date = new Date();
  public lastPaymentDate: Date = new Date();
  public feesPaymentStatus: string = '';
}
