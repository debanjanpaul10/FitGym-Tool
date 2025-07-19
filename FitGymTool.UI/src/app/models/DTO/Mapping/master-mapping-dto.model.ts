import { BugSeverityMappingDto } from './bug-severity-mapping-dto.model';
import { FeesDurationMappingDto } from './fees-duration-mapping-dto.model';
import { FeesPaymentStatusMappingDto } from './fees-payment-status-mapping-dto.model';
import { MembershipStatusMappingDto } from './membership-status-mapping-dto.model';

/**
 * Master data transfer object that contains all mapping data for the application.
 * This DTO serves as a container for various mapping configurations including
 * fees duration, payment status, membership status, and bug severity mappings.
 * Used to centralize and manage all dropdown and reference data across the application.
 */
export class MasterMappingDataDto {
  public feesDurationMapping: FeesDurationMappingDto[] = [];
  public feesPaymentStatusMapping: FeesPaymentStatusMappingDto[] = [];
  public membershipStatusMapping: MembershipStatusMappingDto[] = [];
  public bugSeverityMapping: BugSeverityMappingDto[] = [];
}
