import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { MemberDetailsDto } from '@models/DTO/memberdetails-dto.model';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  private mappingMasterDataSubject$: BehaviorSubject<MasterMappingDataDto> =
    new BehaviorSubject<MasterMappingDataDto>(new MasterMappingDataDto());
  private memberDetailsSubject$: BehaviorSubject<MemberDetailsDto[]> =
    new BehaviorSubject<MemberDetailsDto[]>([]);

  public get MappingMasterData(): Observable<MasterMappingDataDto> {
    return this.mappingMasterDataSubject$.asObservable();
  }

  public set MappingMasterData(data: MasterMappingDataDto) {
    this.mappingMasterDataSubject$.next(data);
  }

  public get MemberDetailsData(): Observable<MemberDetailsDto[]> {
    return this.memberDetailsSubject$.asObservable();
  }

  public set MemberDetailsData(data: MemberDetailsDto[]) {
    this.memberDetailsSubject$.next(data);
  }
}
