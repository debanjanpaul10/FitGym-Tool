import { Injectable, signal, WritableSignal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class DrawerService {
  public isDrawerOpen: WritableSignal<boolean> = signal(false);

  public openDrawer(): void {
    this.isDrawerOpen.set(true);
  }

  public closeDrawer(): void {
    this.isDrawerOpen.set(false);
  }

  public toggleDrawer(): void {
    this.isDrawerOpen.set(!this.isDrawerOpen());
  }
}
