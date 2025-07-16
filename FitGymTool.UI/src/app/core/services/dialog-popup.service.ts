import { Injectable, signal, WritableSignal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DialogPopupService {
  public isAddMemberDialogOpen: WritableSignal<boolean> = signal(false);

  public openAddMemberDialog(): void {
    this.isAddMemberDialogOpen.set(true);
  }

  public closeAddMemberDialog(): void {
    this.isAddMemberDialogOpen.set(false);
  }
}
