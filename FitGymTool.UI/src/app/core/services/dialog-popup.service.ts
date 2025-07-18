import { Injectable, signal, WritableSignal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DialogPopupService {
  public isAddMemberDialogOpen: WritableSignal<boolean> = signal(false);
  public isBugReportDialogOpen: WritableSignal<boolean> = signal(false);
  public isUpdateMembershipDialogOpen: WritableSignal<boolean> = signal(false);

  public openAddMemberDialog(): void {
    this.isAddMemberDialogOpen.set(true);
  }

  public closeAddMemberDialog(): void {
    this.isAddMemberDialogOpen.set(false);
  }

  public openBugReportDialog(): void {
    this.isBugReportDialogOpen.set(true);
  }

  public closeBugReportDialog(): void {
    this.isBugReportDialogOpen.set(false);
  }

  public openMembershipStatusDialog(): void {
    this.isUpdateMembershipDialogOpen.set(true);
  }

  public closeMembershipStatusDialog(): void {
    this.isUpdateMembershipDialogOpen.set(false);
  }
}
