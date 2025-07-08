import { Component, inject, OnInit } from '@angular/core';
import {
  NavigationCancel,
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
  RouterOutlet,
} from '@angular/router';

import { LeftNavigationComponent } from '@components/left-navigation-component/left-navigation-component';
import { RouteConstants } from '@shared/application.constants';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LeftNavigationComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  public isLoginPage: boolean = false;
  public isRouteLoading: boolean = false;

  private router = inject(Router);

  constructor() {
    const path = window.location.pathname;
    this.isLoginPage = path === RouteConstants.Login.Link;

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.isRouteLoading = true;
        this.isLoginPage = event.url === RouteConstants.Login.Link;
      }

      if (
        event instanceof NavigationEnd ||
        event instanceof NavigationCancel ||
        event instanceof NavigationError
      ) {
        this.isRouteLoading = false;
      }
    });
  }

  ngOnInit(): void {}
}
