import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

/**
 * Service for managing global loading states throughout the application.
 * This service provides centralized control over loading indicators and spinners,
 * allowing components to show/hide loading states consistently across the app.
 * Uses BehaviorSubject to emit loading state changes that components can subscribe to.
 */
@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  public loadingSubject: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(true);

  /**
   * Activates the loading state by emitting true to all subscribers.
   * This will typically show loading spinners or skeleton screens in components
   * that are subscribed to the loading state.
   */
  public loadingOn(): void {
    this.loadingSubject.next(true);
  }

  /**
   * Deactivates the loading state by emitting false to all subscribers.
   * This will typically hide loading spinners or skeleton screens in components
   * that are subscribed to the loading state.
   */
  public loadingOff(): void {
    this.loadingSubject.next(false);
  }
}
