import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { MasterMappingDataDto } from '@models/DTO/Mapping/master-mapping-dto.model';
import { MemberDetailsDto } from '@models/DTO/members/memberdetails-dto.model';

/**
 * Common service that manages shared application data and provides centralized access to master mapping data and member details.
 * This service acts as a data store for commonly used information across components, using BehaviorSubjects to provide
 * reactive data streams. It handles master mapping data for dropdowns and forms, as well as member details data
 * that can be shared between different parts of the application.
 */
@Injectable({
  providedIn: 'root',
})
export class CommonService {
  private mappingMasterDataSubject$: BehaviorSubject<MasterMappingDataDto> =
    new BehaviorSubject<MasterMappingDataDto>(new MasterMappingDataDto());
  private memberDetailsSubject$: BehaviorSubject<MemberDetailsDto[]> =
    new BehaviorSubject<MemberDetailsDto[]>([]);

  /**
   * Gets the master mapping data as an observable stream.
   * Provides reactive access to master mapping data for components to subscribe to changes.
   *
   * @returns The observable of master mapping data dto.
   */
  public get MappingMasterData(): Observable<MasterMappingDataDto> {
    return this.mappingMasterDataSubject$.asObservable();
  }

  /**
   * Sets the master mapping data and notifies all subscribers.
   * Updates the internal BehaviorSubject with new master mapping data.
   *
   * @param data The master mapping data dto.
   */
  public set MappingMasterData(data: MasterMappingDataDto) {
    this.mappingMasterDataSubject$.next(data);
  }

  /**
   * Gets the member details data as an observable stream.
   * Provides reactive access to member details data for components to subscribe to changes.
   *
   * @returns The observable for list of member details dto list.
   */
  public get MemberDetailsData(): Observable<MemberDetailsDto[]> {
    return this.memberDetailsSubject$.asObservable();
  }

  /**
   * Sets the member details data and notifies all subscribers.
   * Updates the internal BehaviorSubject with new member details data.
   *
   * @param data The list of member details data dto
   */
  public set MemberDetailsData(data: MemberDetailsDto[]) {
    this.memberDetailsSubject$.next(data);
  }

  /**
   * Subscribes to a specific mapping key in MappingMasterData and handles data availability.
   * If the requested mapping data is available, it calls the setOptions callback with the data.
   * If the mapping data is not available or empty, it calls the fetchMappings fallback function
   * to trigger data retrieval. This method provides a convenient way for components to reactively
   * access specific mapping data while handling the case where data needs to be fetched.
   *
   * @param mappingKey The mapping key
   * @param setOptions The setting options
   * @param fetchMappings The method to fetch actual data if not present
   *
   * @returns The subscription data.
   */
  public subscribeToMapping<T>(
    mappingKey: keyof MasterMappingDataDto,
    setOptions: (options: T[]) => void,
    fetchMappings: () => void
  ) {
    return this.MappingMasterData.subscribe(
      (data: MasterMappingDataDto | null) => {
        const mapping = data && data[mappingKey];
        if (mapping && Object.values(mapping).length > 0) {
          setOptions(mapping as T[]);
        } else {
          fetchMappings();
        }
      }
    );
  }
}
