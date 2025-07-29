export class CurrentMembersFeesStatusDTO {
  public memberId: number = 0;
  public memberName: string = '';
  public memberEmail: string = '';
  public memberStatus: string = '';
  public dueAmount: number = 0.0;
  public dueDate: Date | null = null;
  public paidAmount: number = 0.0;
  public feesPaymentStatus: string = '';
}
