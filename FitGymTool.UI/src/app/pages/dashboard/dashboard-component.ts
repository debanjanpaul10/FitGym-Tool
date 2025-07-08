import { Component } from '@angular/core';
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
export class DashboardComponent {}
