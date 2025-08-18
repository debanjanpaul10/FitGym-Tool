import { MemberDetailsDto } from '@models/DTO/members/memberdetails-dto.model';

// Extended interface to include membershipStatusId for dropdown binding
export interface MemberDetailsWithStatusId extends MemberDetailsDto {
  membershipStatusId: number | null;
}
