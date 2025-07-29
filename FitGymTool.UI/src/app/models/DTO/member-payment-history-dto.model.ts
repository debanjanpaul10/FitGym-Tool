export class MemberPaymentHistoryDTO {
  memberId: number = 0;
  memberName: string = '';
  memberEmail: string = '';
  memberStatus: string = '';
  amount: number = 0.0;
  fromDate: Date = new Date();
  toDate: Date = new Date();
  feesPaymentStatus: string = '';
}
