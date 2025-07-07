import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  MSAL_GUARD_CONFIG,
  MsalBroadcastService,
  MsalGuardConfiguration,
  MsalService,
} from '@azure/msal-angular';
import { RedirectRequest } from '@azure/msal-browser';
import {
  LoginPageConstants,
  RouteConstants,
} from '@shared/application.constants';
import { ButtonModule } from 'primeng/button';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-login',
  imports: [ButtonModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit, OnDestroy {
  public HeaderConstants = LoginPageConstants.Headings;

  private readonly _destroying$ = new Subject<void>();

  private readonly authService = inject(MsalService);
  private readonly msalGuarConfig =
    inject<MsalGuardConfiguration>(MSAL_GUARD_CONFIG);
  private readonly msalBroadCastService = inject(MsalBroadcastService);
  private readonly router = inject(Router);

  ngOnInit(): void {}

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

  ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }
}
