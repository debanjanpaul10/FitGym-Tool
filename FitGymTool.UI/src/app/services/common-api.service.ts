import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '@environments/environment';
import { AddBugReportDto } from '@models/DTO/add-bug-report-dto.model';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ApiRoutes } from '@shared/routes.constants';

@Injectable({
  providedIn: 'root',
})
export class CommonApiService {
  private commonApiRoute = ApiRoutes.CommonApi;
  private apiBaseUrl: string = `${environment.apiBaseUrl}/${this.commonApiRoute.BaseRoute}`;

  private readonly httpClient: HttpClient = inject(HttpClient);

  public GetMappingsMasterDataAsync(): Observable<ResponseDto> {
    const apiUrl: string = `${this.apiBaseUrl}${this.commonApiRoute.GetMappingsMasterData_ApiRoute}`;
    return this.httpClient.get<ResponseDto>(apiUrl);
  }

  public AddBugReportDataAsync(
    bugReportData: AddBugReportDto
  ): Observable<ResponseDto> {
    const apiUrl: string = `${this.apiBaseUrl}${this.commonApiRoute.AddBugReport_ApiRoute}`;
    return this.httpClient.post<ResponseDto>(apiUrl, bugReportData);
  }
}
