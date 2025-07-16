import { CommonModule } from '@angular/common';
import {
  Component,
  inject,
  OnInit,
  signal,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { ButtonModule } from 'primeng/button';
import { Drawer, DrawerModule } from 'primeng/drawer';
import { Router } from '@angular/router';

import { DrawerConstants, RouteConstants } from '@shared/application.constants';
import { DrawerService } from '@core/services/drawer.service';

@Component({
  selector: 'app-left-navigation-component',
  imports: [DrawerModule, ButtonModule, CommonModule],
  templateUrl: './left-navigation.component.html',
  styleUrl: './left-navigation.component.scss',
})
export class LeftNavigationComponent implements OnInit {
  @ViewChild('drawerRef') drawerRef!: Drawer;

  protected DrawerConstants = DrawerConstants;
  protected isVisible: WritableSignal<boolean> = signal(false);
  protected menuItems: any;

  private readonly authService: MsalService = inject(MsalService);
  private readonly drawerService: DrawerService = inject(DrawerService);
  private readonly routerService: Router = inject(Router);

  constructor() {
    this.isVisible = this.drawerService.isDrawerOpen;
  }

  ngOnInit(): void {
    this.menuItems = [
      {
        name: 'Member Management',
        icon: 'pi pi-users',
        onClick: () => this.navigateUsersDashboard(),
      },

      {
        name: 'Fees Management',
        icon: 'pi pi-wallet',
        onClick: () => this.navigateFeesDashboard(),
      },
      {
        name: 'Facility Management',
        icon: 'pi pi-hammer',
        onClick: () => this.navigateFacilityDashboard(),
      },
      {
        name: 'Logout',
        icon: 'pi pi-sign-out',
        onClick: () => this.logoutRedirect(),
      },
    ];
  }

  public closeCallback(e: any): void {
    this.drawerRef.close(e);
    this.drawerService.closeDrawer();
  }

  public homePageRedirect(): void {
    this.routerService.navigate([RouteConstants.Dashboard.Link]);
    this.drawerService.closeDrawer();
  }

  private logoutRedirect(): void {
    this.authService.logoutRedirect();
    this.drawerService.closeDrawer();
  }

  private navigateUsersDashboard(): void {
    this.routerService.navigate([RouteConstants.MemberManagement.Link]);
    this.drawerService.closeDrawer();
  }

  private navigateFeesDashboard(): void {
    this.routerService.navigate([RouteConstants.FeesManagement.Link]);
    this.drawerService.closeDrawer();
  }

  private navigateFacilityDashboard(): void {
    this.routerService.navigate([RouteConstants.FacilityManagement.Link]);
    this.drawerService.closeDrawer();
  }
}
