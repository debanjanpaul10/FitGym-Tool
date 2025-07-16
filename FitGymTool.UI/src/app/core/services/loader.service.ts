import { Injectable, signal, WritableSignal } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  public loadingSubject: BehaviorSubject<boolean> =
    new BehaviorSubject<boolean>(true);

  public loadingOn(): void {
    this.loadingSubject.next(true);
  }

  public loadingOff(): void {
    this.loadingSubject.next(false);
  }
}
