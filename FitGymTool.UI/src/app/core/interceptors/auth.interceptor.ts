import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { catchError, from, Observable, switchMap, throwError } from 'rxjs';

import { environment } from '@environments/environment';
import { ConfigurationConstants } from '@shared/application.constants';

/**
 * HTTP interceptor that handles authentication for outgoing requests.
 * Automatically adds Bearer tokens to requests and handles token refresh scenarios.
 */
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private authService = inject(MsalService);
  private authenticatorConstants =
    ConfigurationConstants.AuthenticatorConstants;

  /**
   * Intercepts HTTP requests to add authentication headers.
   * Handles token acquisition, refresh, and fallback authentication scenarios.
   * @param request - The outgoing HTTP request to be intercepted
   * @param next - The next handler in the interceptor chain
   * @returns Observable of HTTP events with authentication headers added
   */
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const excludeUrl = environment.apiConfig.uri;
    if (request.urlWithParams.includes(excludeUrl)) {
      return next.handle(request);
    }

    const isGraphApi = request.url.startsWith(
      this.authenticatorConstants.GraphApiUrl
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
            error.errorCode ===
              this.authenticatorConstants.InvalidGrantConstant &&
            error.errorMessage &&
            error.errorMessage.includes(
              this.authenticatorConstants.InvalidGrantError
            )
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
