export class CurrentMonthFeesAndRevenueStatus {
  public memberId: number = 0;
  public memberEmail: string = '';
  public membershipStatus: string = '';
  public amount: number = 0.0;
  public fromDate: Date = new Date();
  public toDate: Date = new Date();
  public feesStatus: string = '';
}
