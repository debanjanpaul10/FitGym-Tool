import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '@environments/environment';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ApiRoutes } from '@shared/routes.constants';

@Injectable({
  providedIn: 'root',
})
export class MemberFeesApiService {
  private memberFeesApiRoute = ApiRoutes.MemberFeesApi;
  private apiBaseUrl = `${environment.apiBaseUrl}/${this.memberFeesApiRoute.BaseRoute}`;

  private readonly httpClient: HttpClient = inject(HttpClient);

  public GetCurrentMonthFeesAndRevenueStatusAsync(): Observable<ResponseDto> {
    const apiUrl = `${this.apiBaseUrl}${this.memberFeesApiRoute.GetCurrentMonthFeesAndRevenueStatus_ApiRoute}`;
    return this.httpClient.get<ResponseDto>(apiUrl);
  }

  public GetCurrentFeesStructureAsync(): Observable<ResponseDto> {
    const apiUrl = `${this.apiBaseUrl}${this.memberFeesApiRoute.GetCurrentFeesStructure_ApiRoute}`;
    return this.httpClient.get<ResponseDto>(apiUrl);
  }

  public GetCurrentMembersFeesStatusAsync(): Observable<ResponseDto> {
    const apiUrl = `${this.apiBaseUrl}${this.memberFeesApiRoute.GetCurrentMembersFeesStatus_ApiRoute}`;
    return this.httpClient.get<ResponseDto>(apiUrl);
  }

  public GetPaymentHistoryDataForMemberAsync(
    userEmail: string = ''
  ): Observable<ResponseDto> {
    const apiUrl = `${this.apiBaseUrl}${this.memberFeesApiRoute.GetPaymentHistoryDataForMember_ApiRoute}${userEmail}`;
    return this.httpClient.get<ResponseDto>(apiUrl);
  }
}
