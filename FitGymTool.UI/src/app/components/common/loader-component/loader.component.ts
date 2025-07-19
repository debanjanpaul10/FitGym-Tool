import { Component, inject, Input, OnDestroy, OnInit } from '@angular/core';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import {
  RouteConfigLoadEnd,
  RouteConfigLoadStart,
  Router,
  NavigationStart,
  NavigationEnd,
  NavigationCancel,
  NavigationError,
  Event as RouterEvent,
} from '@angular/router';
import { Observable, filter, Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

import { LoaderService } from '@core/services/loader.service';

/**
 * Loader component that displays a loading spinner during application operations.
 * This component provides visual feedback to users during data loading, API calls,
 * and route transitions. It integrates with the loader service to manage loading states
 * and can optionally detect route transitions to automatically show/hide the loader.
 */
@Component({
  selector: 'app-loader-component',
  imports: [CommonModule, ProgressSpinnerModule],
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.scss',
})
export class LoaderComponent implements OnInit, OnDestroy {
  @Input() detectRouteTransitions: boolean = false;

  protected readonly isLoading$: Observable<boolean>;

  private routerSubscription?: Subscription;
  private readonly loaderService: LoaderService = inject(LoaderService);
  private readonly routerService: Router = inject(Router);

  constructor() {
    this.isLoading$ = this.loaderService.loadingSubject;
  }

  ngOnInit(): void {
    if (this.detectRouteTransitions) {
      this.subscribeToRouterEvents();
    }
  }

  ngOnDestroy(): void {
    this.routerSubscription?.unsubscribe();
  }

  /**
   * Subscribes to router events to automatically manage loading states during navigation.
   * Filters for relevant navigation events and toggles the loader on/off based on event type.
   * Loading starts on RouteConfigLoadStart and NavigationStart events, and stops on
   * RouteConfigLoadEnd, NavigationEnd, NavigationCancel, and NavigationError events.
   */
  private subscribeToRouterEvents(): void {
    const loadingStartEvents = [RouteConfigLoadStart, NavigationStart];
    const loadingEndEvents = [
      RouteConfigLoadEnd,
      NavigationEnd,
      NavigationCancel,
      NavigationError,
    ];

    this.routerSubscription = this.routerService.events
      .pipe(
        filter(
          (event: RouterEvent) =>
            loadingStartEvents.some(
              (eventType) => event instanceof eventType
            ) ||
            loadingEndEvents.some((eventType) => event instanceof eventType)
        )
      )
      .subscribe((event: RouterEvent) => {
        if (
          loadingStartEvents.some((eventType) => event instanceof eventType)
        ) {
          this.loaderService.loadingOn();
        } else {
          this.loaderService.loadingOff();
        }
      });
  }
}
