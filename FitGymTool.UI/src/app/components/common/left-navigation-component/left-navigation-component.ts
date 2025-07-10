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
import { DrawerConstants } from '@shared/application.constants';
import { ButtonModule } from 'primeng/button';
import { Drawer, DrawerModule } from 'primeng/drawer';
import { DrawerService } from '@services/drawer.service';

@Component({
  selector: 'app-left-navigation-component',
  imports: [DrawerModule, ButtonModule, CommonModule],
  templateUrl: './left-navigation-component.html',
  styleUrl: './left-navigation-component.scss',
})
export class LeftNavigationComponent implements OnInit {
  @ViewChild('drawerRef') drawerRef!: Drawer;

  public DrawerConstants = DrawerConstants;
  public isVisible: WritableSignal<boolean>;
  public menuItems: any;

  private readonly authService = inject(MsalService);
  private readonly drawerService = inject(DrawerService);

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

  private logoutRedirect(): void {
    this.authService.logoutRedirect();
  }

  private navigateUsersDashboard(): void {
    alert('Feature being worked on');
  }

  private navigateFeesDashboard(): void {
    alert('Feature being worked on');
  }

  private navigateFacilityDashboard(): void {
    alert('Feature being worked on');
  }
}
