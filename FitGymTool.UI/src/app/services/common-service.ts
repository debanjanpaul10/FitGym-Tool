import { Injectable } from '@angular/core';
import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  private mappingMasterDataSubject$ = new BehaviorSubject<MasterMappingDataDto>(
    new MasterMappingDataDto()
  );

  public get MappingMasterData(): Observable<MasterMappingDataDto> {
    return this.mappingMasterDataSubject$.asObservable();
  }

  public set MappingMasterData(data: MasterMappingDataDto) {
    this.mappingMasterDataSubject$.next(data);
  }
}
