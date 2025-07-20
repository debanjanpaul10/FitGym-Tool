import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '@environments/environment';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ApiRoutes } from '@shared/routes.constants';
import { AddMemberDto } from '@models/DTO/members/add-member-dto.model';
import { UpdateMembershipStatusDto } from '@models/DTO/members/update-membership-status-dto.model';
import { UpdateMemberDto } from '@models/DTO/members/update-member-dto.model';

@Injectable({
  providedIn: 'root',
})
export class MembersApiService {
  private membersApiRoutes = ApiRoutes.MembersApi;
  private apiBaseUrl: string = `${environment.apiBaseUrl}/${this.membersApiRoutes.BaseRoute}`;

  private readonly httpClient: HttpClient = inject(HttpClient);

  public GetAllMembersAsync(): Observable<ResponseDto> {
    const apiUrl = `${this.apiBaseUrl}${this.membersApiRoutes.GetAllMembers_ApiRoute}`;
    return this.httpClient.get<ResponseDto>(apiUrl);
  }

  public GetMemberByEmailIdAsync(
    emailAddress: string
  ): Observable<ResponseDto> {
    const apiUrl = `${this.apiBaseUrl}${this.membersApiRoutes.GetMemberByEmailId_ApiRoute}`;
    return this.httpClient.post<ResponseDto>(apiUrl, emailAddress);
  }

  public AddNewMemberAsync_FromAdmin(
    newMemberData: AddMemberDto
  ): Observable<ResponseDto> {
    const apiUrl = `${this.apiBaseUrl}${
      this.membersApiRoutes.AddMember_ApiRoute
    }${true}`;
    return this.httpClient.post<ResponseDto>(apiUrl, newMemberData);
  }

  public UpdateMembershipStatusAsync(
    updateMembershipStatus: UpdateMembershipStatusDto
  ): Observable<ResponseDto> {
    const apiUrl = `${this.apiBaseUrl}${this.membersApiRoutes.UpdateMembershipDetails_ApiRoute}`;
    return this.httpClient.put<ResponseDto>(apiUrl, updateMembershipStatus);
  }

  public UpdateMemberDetailsAsync(
    updateMemberDetailsDto: UpdateMemberDto
  ): Observable<ResponseDto> {
    const apiUrl = `${this.apiBaseUrl}${this.membersApiRoutes.UpdateMemberDetails_ApiRoute}`;
    return this.httpClient.put<ResponseDto>(apiUrl, updateMemberDetailsDto);
  }
}
