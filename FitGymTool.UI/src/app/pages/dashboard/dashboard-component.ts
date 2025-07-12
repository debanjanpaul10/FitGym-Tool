import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { AccountInfo } from '@azure/msal-browser';
import { ActiveMembersComponent } from '@components/dashboard/active-members-component/active-members-component';
import { CurrentRevenueComponent } from '@components/dashboard/current-revenue-component/current-revenue-component';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-dashboard',
  imports: [
    CardModule,
    ButtonModule,
    CurrentRevenueComponent,
    ActiveMembersComponent,
  ],
  templateUrl: './dashboard-component.html',
  styleUrl: './dashboard-component.scss',
})
export class DashboardComponent implements OnInit {
  public currentUserProfile: AccountInfo | null = null;
  public currentUserName: WritableSignal<string> = signal('');

  private readonly msalService: MsalService = inject(MsalService);

  ngOnInit(): void {
    this.currentUserProfile = this.msalService.instance.getActiveAccount();
    if (this.currentUserProfile) {
      this.currentUserName.set(this.currentUserProfile.name ?? '');
    }
  }
}
