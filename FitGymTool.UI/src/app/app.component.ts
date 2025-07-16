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
import { filter } from 'rxjs/operators';

import { HeaderComponent } from '@components/common/header-component/header.component';
import { LeftNavigationComponent } from '@components/common/left-navigation-component/left-navigation.component';
import { LoaderComponent } from '@components/common/loader-component/loader.component';
import { LoaderService } from '@core/services/loader.service';
import { RouteConstants } from '@shared/application.constants';
import { ToasterComponent } from '@components/common/toaster-component/toaster.component';
import { FooterComponent } from '@components/common/footer-component/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    LeftNavigationComponent,
    HeaderComponent,
    CommonModule,
    LoaderComponent,
    ToasterComponent,
    FooterComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  public isLoginPage: WritableSignal<boolean> = signal(false);
  public isRouteLoading: WritableSignal<boolean> = signal(false);

  private router: Router = inject(Router);
  private loaderService: LoaderService = inject(LoaderService);

  constructor() {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.isLoginPage.set(
          event.urlAfterRedirects === RouteConstants.Login.RouteValue
        );
      });

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.isRouteLoading.set(true);
        this.isLoginPage.set(event.url === RouteConstants.Login.RouteValue);
      }

      if (
        event instanceof NavigationEnd ||
        event instanceof NavigationCancel ||
        event instanceof NavigationError
      ) {
        this.isRouteLoading.set(false);
      }
    });
  }

  ngOnInit(): void {
    this.loaderService.loadingOn();
  }
}
