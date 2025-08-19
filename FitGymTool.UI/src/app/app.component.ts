import { CommonModule } from '@angular/common';
import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import {
  NavigationCancel,
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
  RouterOutlet,
} from '@angular/router';

import { BugReportComponent } from '@components/bug-report/bug-report.component';
import { ChatComponent } from '@components/chat-component/chat-component';
import { FooterComponent } from '@components/common/footer-component/footer.component';
import { HeaderComponent } from '@components/common/header-component/header.component';
import { LeftNavigationComponent } from '@components/common/left-navigation-component/left-navigation.component';
import { LoaderComponent } from '@components/common/loader-component/loader.component';
import { ToasterComponent } from '@components/common/toaster-component/toaster.component';
import { DrawerService } from '@core/services/drawer.service';
import { LoaderService } from '@core/services/loader.service';
import { RouteConstants } from '@shared/routes.constants';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    BugReportComponent,
    ChatComponent,
    FooterComponent,
    HeaderComponent,
    LeftNavigationComponent,
    LoaderComponent,
    ToasterComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  protected readonly isLoginPage: WritableSignal<boolean> = signal(false);
  protected readonly isRouteLoading: WritableSignal<boolean> = signal(false);

  private readonly _router = inject(Router);
  private readonly _loaderService = inject(LoaderService);
  protected readonly _drawerService = inject(DrawerService);

  constructor() {
    this.initializeRouterEvents();
  }

  ngOnInit(): void {
    this._loaderService.loadingOn();
  }

  protected shouldShowDrawer(): boolean {
    return !this.isLoginPage() && !this.isRouteLoading();
  }

  private initializeRouterEvents(): void {
    this._router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.isRouteLoading.set(true);
        this.isLoginPage.set(event.url === RouteConstants.Login.RouteValue);
      }

      if (event instanceof NavigationEnd) {
        this.isRouteLoading.set(false);
        this.isLoginPage.set(
          event.urlAfterRedirects === RouteConstants.Login.RouteValue
        );
      }

      if (
        event instanceof NavigationCancel ||
        event instanceof NavigationError
      ) {
        this.isRouteLoading.set(false);
      }
    });
  }
}
