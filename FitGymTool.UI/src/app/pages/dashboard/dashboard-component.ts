import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

import { ActiveMembersComponent } from '@components/dashboard/active-members-component/active-members-component';
import { CurrentRevenueComponent } from '@components/dashboard/current-revenue-component/current-revenue-component';

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
export class DashboardComponent {}
