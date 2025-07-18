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
import { DialogPopupService } from '@core/services/dialog-popup.service';

/**
 * Left navigation component that provides a drawer-based side navigation menu.
 * This component displays the main navigation menu with options for member management,
 * fees management, facility management, bug reporting, and logout functionality.
 * It integrates with the drawer service to manage visibility state and provides
 * navigation to different sections of the application.
 */
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
  private readonly dialogPopupService: DialogPopupService =
    inject(DialogPopupService);

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
        name: 'Report a bug',
        icon: 'pi pi-exclamation-triangle',
        onClick: () => this.handleBugReport(),
      },
      {
        name: 'Logout',
        icon: 'pi pi-sign-out',
        onClick: () => this.logoutRedirect(),
      },
    ];
  }

  /**
   * Handles the drawer close event and updates the drawer service state.
   * Called when the drawer is closed either by user interaction or programmatically.
   */
  protected closeCallback(e: any): void {
    this.drawerRef.close(e);
    this.drawerService.closeDrawer();
  }

  /**
   * Navigates to the home/dashboard page and closes the drawer.
   * Used when the user clicks on the home navigation option.
   */
  protected homePageRedirect(): void {
    this.routerService.navigate([RouteConstants.Dashboard.Link]);
    this.drawerService.closeDrawer();
  }

  // #region PRIVATE METHODS

  /**
   * Opens the bug report dialog and closes the navigation drawer.
   * Triggered when the user selects the "Report a bug" menu option.
   */
  private handleBugReport(): void {
    this.dialogPopupService.openBugReportDialog();
    this.drawerService.closeDrawer();
  }

  /**
   * Initiates user logout and closes the navigation drawer.
   * Uses MSAL service to redirect the user to the logout flow.
   */
  private logoutRedirect(): void {
    this.authService.logoutRedirect();
    this.drawerService.closeDrawer();
  }

  /**
   * Navigates to the member management dashboard and closes the drawer.
   * Handles navigation when the "Member Management" menu item is selected.
   */
  private navigateUsersDashboard(): void {
    this.routerService.navigate([RouteConstants.MemberManagement.Link]);
    this.drawerService.closeDrawer();
  }

  /**
   * Navigates to the fees management dashboard and closes the drawer.
   * Handles navigation when the "Fees Management" menu item is selected.
   */
  private navigateFeesDashboard(): void {
    this.routerService.navigate([RouteConstants.FeesManagement.Link]);
    this.drawerService.closeDrawer();
  }

  /**
   * Navigates to the facility management dashboard and closes the drawer.
   * Handles navigation when the "Facility Management" menu item is selected.
   */
  private navigateFacilityDashboard(): void {
    this.routerService.navigate([RouteConstants.FacilityManagement.Link]);
    this.drawerService.closeDrawer();
  }

  // #endregion
}
