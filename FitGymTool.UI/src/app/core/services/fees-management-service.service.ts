import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

import { CurrentMembersFeesStatusDTO } from '@models/DTO/current-members-fees-status-dto.model';

@Injectable({
  providedIn: 'root',
})
export class FeesManagementService {
  private _currentMemberFeesData$: BehaviorSubject<
    CurrentMembersFeesStatusDTO[]
  > = new BehaviorSubject<CurrentMembersFeesStatusDTO[]>([]);

  public set currentMemberFees(data: CurrentMembersFeesStatusDTO[]) {
    this._currentMemberFeesData$.next(data);
  }

  public get currentMemberFees(): Observable<CurrentMembersFeesStatusDTO[]> {
    return this._currentMemberFeesData$.asObservable();
  }
}
