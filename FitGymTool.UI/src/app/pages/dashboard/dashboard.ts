import { Component, inject } from '@angular/core';
import { DashboardPageConstants } from '@shared/application.constants';
import { ButtonModule } from 'primeng/button';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-dashboard',
  imports: [ButtonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {
  public HeaderConstants = DashboardPageConstants.Headings;

  private readonly authService = inject(MsalService);

  public logoutRedirect(): void {
    this.authService.logoutRedirect();
  }
}
