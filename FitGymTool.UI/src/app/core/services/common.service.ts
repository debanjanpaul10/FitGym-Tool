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

  /**
   * Subscribes to a mapping key in MappingMasterData, sets options if present, otherwise calls fallback to fetch mappings.
   * @param mappingKey The key of the mapping in MasterMappingDataDto (e.g., 'bugSeverityMapping')
   * @param setOptions Callback to set the options in the component
   * @param fetchMappings Fallback function to fetch mappings if not present
   * @returns Subscription
   */
  public subscribeToMapping<T>(
    mappingKey: keyof MasterMappingDataDto,
    setOptions: (options: T[]) => void,
    fetchMappings: () => void
  ) {
    return this.MappingMasterData.subscribe((data: MasterMappingDataDto | null) => {
      const mapping = data && data[mappingKey];
      if (mapping && Object.values(mapping).length > 0) {
        setOptions(mapping as T[]);
      } else {
        fetchMappings();
      }
    });
  }
}
