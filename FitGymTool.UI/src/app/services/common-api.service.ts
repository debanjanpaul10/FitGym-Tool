import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '@environments/environment';
import { AddBugReportDTO } from '@models/DTO/add-bug-report-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ApiRoutes } from '@shared/routes.constants';
import { BugSeverityInputDTO } from '@models/DTO/bug-severity-input-dto.model';

@Injectable({
  providedIn: 'root',
})
export class CommonApiService {
  private _commonApiRoute = ApiRoutes.CommonApi;
  private _apiBaseUrl: string = `${environment.apiBaseUrl}/${this._commonApiRoute.BaseRoute}`;

  private readonly _httpClient: HttpClient = inject(HttpClient);

  public GetMappingsMasterDataAsync(): Observable<ResponseDto> {
    const apiUrl = `${this._apiBaseUrl}${this._commonApiRoute.GetMappingsMasterData_ApiRoute}`;
    return this._httpClient.get<ResponseDto>(apiUrl);
  }

  public AddBugReportDataAsync(
    bugReportData: AddBugReportDTO
  ): Observable<ResponseDto> {
    const apiUrl = `${this._apiBaseUrl}${this._commonApiRoute.AddBugReport_ApiRoute}`;
    return this._httpClient.post<ResponseDto>(apiUrl, bugReportData);
  }
}
