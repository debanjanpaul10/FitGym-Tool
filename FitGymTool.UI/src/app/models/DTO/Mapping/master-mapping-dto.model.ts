import { FeesDurationMappingDto } from './fees-duration-mapping-dto.model';
import { FeesPaymentStatusMappingDto } from './fees-payment-status-mapping-dto.model';
import { MembershipStatusMappingDto } from './membership-status-mapping-dto.model';

export class MasterMappingDataDto {
  public feesDurationMapping: FeesDurationMappingDto[] = [];

  public feesPaymentStatusMapping: FeesPaymentStatusMappingDto[] = [];

  public membershipStatusMapping: MembershipStatusMappingDto[] = [];
}
