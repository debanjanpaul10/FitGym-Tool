import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '@environments/environment';
import { ApiRoutes } from '@shared/routes.constants';
import { ResponseDto } from '@models/DTO/response-dto.model';
import { ChatMessageRequestDTO } from '@models/DTO/chat-message-request-dto.model';
import { BugSeverityInputDTO } from '@models/DTO/bug-severity-input-dto.model';

@Injectable({
  providedIn: 'root',
})
export class AiApiService {
  private readonly _aiServiceApiRoute = ApiRoutes.AIServicesApi;
  private _apiBaseUrl: string = `${environment.apiBaseUrl}/${this._aiServiceApiRoute.BaseRoute}`;

  private readonly _httpClient: HttpClient = inject(HttpClient);

  public RespondAsync(
    userMessage: ChatMessageRequestDTO
  ): Observable<ResponseDto> {
    const apiUrl = `${this._apiBaseUrl}${this._aiServiceApiRoute.Respond_ApiRoute}`;
    return this._httpClient.post<ResponseDto>(apiUrl, userMessage);
  }

  public GetBugSeverityStatusAsync(
    bugSeverityInput: BugSeverityInputDTO
  ): Observable<ResponseDto> {
    const apiUrl = `${this._apiBaseUrl}${this._aiServiceApiRoute.GetBugSeverityStatus_ApiRoute}`;
    return this._httpClient.post<ResponseDto>(apiUrl, bugSeverityInput);
  }

  public GetActiveAIFeaturesAsync(): Observable<ResponseDto> {
    const apiUrl = `${this._apiBaseUrl}${this._aiServiceApiRoute.GetActiveAIFeatures_ApiRoute}`;
    return this._httpClient.get<ResponseDto>(apiUrl);
  }
}
