import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '@environments/environment.development';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ApiRoutes } from '@shared/routes.constants';

@Injectable({
  providedIn: 'root',
})
export class MembersApiService {
  public membersApiRoutes = ApiRoutes.MembersApi;

  private readonly httpClient: HttpClient = inject(HttpClient);

  private apiBaseUrl: string = `${environment.apiBaseUrl}/${this.membersApiRoutes.BaseRoute}`;

  public GetAllMembersAsync(): Observable<ResponseDto> {
    const apiUrl: string = `${this.apiBaseUrl}${this.membersApiRoutes.GetAllMembers_ApiRoute}`;
    return this.httpClient.get<ResponseDto>(apiUrl);
  }
}
