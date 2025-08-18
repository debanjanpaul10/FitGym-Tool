/**
 * Data transfer object for current month fees and revenue status information.
 * Contains member fee details including payment status, amounts, and date ranges
 * used for revenue tracking and financial reporting in the current month.
 */
export class CurrentMonthFeesAndRevenueStatusDTO {
  public memberId: number = 0;
  public memberEmail: string = '';
  public membershipStatus: string = '';
  public amount: number = 0.0;
  public fromDate: Date = new Date();
  public toDate: Date = new Date();
  public feesStatus: string = '';
}
