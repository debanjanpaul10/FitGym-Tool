import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  MSAL_GUARD_CONFIG,
  MsalBroadcastService,
  MsalGuardConfiguration,
  MsalService,
} from '@azure/msal-angular';
import { RedirectRequest, EventType } from '@azure/msal-browser';
import { ButtonModule } from 'primeng/button';

import {
  LoginPageConstants,
  RouteConstants,
} from '@shared/application.constants';

@Component({
  selector: 'app-login',
  imports: [ButtonModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.scss',
})
export class LoginComponent implements OnInit {
  public HeaderConstants = LoginPageConstants.Headings;

  private readonly authService = inject(MsalService);
  private readonly msalGuarConfig =
    inject<MsalGuardConfiguration>(MSAL_GUARD_CONFIG);
  private readonly msalBroadCastService = inject(MsalBroadcastService);
  private readonly router = inject(Router);

  ngOnInit(): void {
    this.msalBroadCastService.msalSubject$.subscribe((event) => {
      if (event.eventType === EventType.LOGIN_SUCCESS) {
        this.navigateHome();
      }
    });

    if (this.authService.instance.getAllAccounts().length > 0) {
      this.navigateHome();
    }
  }

  public loginRedirect(): void {
    if (this.msalGuarConfig.authRequest) {
      this.authService.loginRedirect({
        ...this.msalGuarConfig.authRequest,
      } as RedirectRequest);
    } else {
      this.authService.loginRedirect();
    }
  }

  public navigateHome(): void {
    this.router.navigate([RouteConstants.Dashboard.Link]);
  }
}
