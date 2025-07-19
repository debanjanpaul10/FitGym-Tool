import {
  MsalGuardConfiguration,
  MsalInterceptorConfiguration,
} from '@azure/msal-angular';
import {
  BrowserCacheLocation,
  InteractionType,
  IPublicClientApplication,
  PublicClientApplication,
} from '@azure/msal-browser';

import { environment } from '@environments/environment.development';
import { RouteConstants } from '@shared/routes.constants';

/**
 * Factory function that creates and configures the MSAL Public Client Application instance.
 * Sets up authentication configuration, cache settings, and system preferences for Azure AD integration.
 * @returns Configured IPublicClientApplication instance for handling authentication
 */
export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: environment.msalConfig.auth.clientId,
      authority: environment.msalConfig.auth.authority,
      redirectUri: '/',
      postLogoutRedirectUri: '/',
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      secureCookies: true,
      storeAuthStateInCookie: true,
    },
    system: {
      allowPlatformBroker: false,
      preventCorsPreflight: true,
    },
  });
}

/**
 * Factory function that creates the MSAL interceptor configuration.
 * Defines protected resources and their required scopes for automatic token attachment.
 * @returns MsalInterceptorConfiguration with protected resource mappings and interaction type
 */
export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, string[]>();
  protectedResourceMap.set(
    environment.apiConfig.uri,
    environment.apiConfig.scopes
  );

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap,
  };
}

/**
 * Factory function that creates the MSAL guard configuration for route protection.
 * Configures authentication requirements, scopes, and fallback routes for protected routes.
 * @returns MsalGuardConfiguration with interaction type, auth request, and failure route settings
 */
export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: [...environment.apiConfig.scopes],
    },
    loginFailedRoute: RouteConstants.Dashboard.Link,
  };
}
