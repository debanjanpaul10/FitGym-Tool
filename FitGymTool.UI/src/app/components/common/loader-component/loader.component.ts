import { Component, inject, Input, OnInit } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {
  RouteConfigLoadEnd,
  RouteConfigLoadStart,
  Router,
  NavigationStart,
  NavigationEnd,
  NavigationCancel,
  NavigationError,
} from '@angular/router';
import { Observable, tap } from 'rxjs';
import { CommonModule } from '@angular/common';

import { LoaderService } from '@core/services/loader.service';

@Component({
  selector: 'app-loader-component',
  imports: [CommonModule, MatProgressSpinnerModule],
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.scss',
})
export class LoaderComponent implements OnInit {
  @Input() detectRouteTransitions: boolean = false;

  public isLoading$: Observable<boolean>;

  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly routerService: Router = inject(Router);

  constructor() {
    this.isLoading$ = this.loaderService.loadingSubject;
  }

  ngOnInit(): void {
    if (this.detectRouteTransitions) {
      this.routerService.events
        .pipe(
          tap((event) => {
            if (
              event instanceof RouteConfigLoadStart ||
              event instanceof NavigationStart
            ) {
              this.loaderService.loadingOn();
            } else if (
              event instanceof RouteConfigLoadEnd ||
              event instanceof NavigationEnd ||
              event instanceof NavigationCancel ||
              event instanceof NavigationError
            ) {
              this.loaderService.loadingOff();
            }
          })
        )
        .subscribe();
    }
  }
}
