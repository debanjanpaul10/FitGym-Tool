import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { catchError, from, Observable, switchMap, throwError } from 'rxjs';

import { environment } from '@environments/environment.development';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private authService = inject(MsalService);

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const excludeUrl = environment.apiConfig.uri;
    if (request.urlWithParams.includes(excludeUrl)) {
      return next.handle(request);
    }

    const isGraphApi = request.url.startsWith(
      'https://graph.microsoft.com/v1.0/users?$filter=startswith(displayName'
    );
    const scopes = isGraphApi
      ? environment.msalConfig.scopes
      : environment.apiConfig.apiScope;
    const account = this.authService.instance.getActiveAccount();

    if (account) {
      return from(
        this.authService.instance.acquireTokenSilent({
          scopes: scopes,
        })
      ).pipe(
        switchMap((response) => {
          request = request.clone({
            setHeaders: {
              Authorization: `Bearer ${response.accessToken}`,
            },
          });

          return next.handle(request);
        }),
        catchError((error: any) => {
          if (
            error &&
            error.errorCode === 'invalid_grant' &&
            error.errorMessage &&
            error.errorMessage.includes('AADSTS65001')
          ) {
            return from(
              this.authService.instance.acquireTokenPopup({
                scopes: scopes,
              })
            ).pipe(
              switchMap((popupResponse: any) => {
                request = request.clone({
                  setHeaders: {
                    Authorization: `Bearer ${popupResponse.accessToken}`,
                  },
                });

                return next.handle(request);
              }),
              catchError((popupError: any) => {
                return throwError(() => popupError);
              })
            );
          }

          return throwError(() => error);
        })
      );
    }

    return next.handle(request);
  }
}
