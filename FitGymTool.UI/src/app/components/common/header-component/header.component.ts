import {
  Component,
  inject,
  OnInit,
  signal,
  WritableSignal,
} from '@angular/core';
import { AvatarModule } from 'primeng/avatar';
import { CommonModule } from '@angular/common';
import { MsalService } from '@azure/msal-angular';
import { ButtonModule } from 'primeng/button';
import { TooltipModule } from 'primeng/tooltip';
import { AccountInfo } from '@azure/msal-browser';
import { Router } from '@angular/router';
import { MessageModule } from 'primeng/message';

import { DrawerService } from '@core/services/drawer.service';
import {
  CommonApplicationConstants,
  DrawerConstants,
} from '@shared/application.constants';
import { RouteConstants } from '@shared/routes.constants';

/**
 * Header component that displays the application header with navigation and user information.
 * This component provides the main navigation header including brand text, user profile information,
 * drawer toggle functionality, and home page navigation. It integrates with MSAL for user authentication
 * and displays the current user's profile information.
 */
@Component({
  selector: 'app-header-component',
  imports: [
    AvatarModule,
    CommonModule,
    ButtonModule,
    TooltipModule,
    MessageModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  protected BrandText = DrawerConstants.Headings.BrandText;
  protected currentUserProfile: AccountInfo | null = null;
  protected currentUserName: WritableSignal<string> = signal('');
  protected AiFeaturesMessage =
    CommonApplicationConstants.HeaderConstants.AIFeaturesMessage;

  private readonly msalService: MsalService = inject(MsalService);
  private readonly drawerService: DrawerService = inject(DrawerService);
  private readonly routerService: Router = inject(Router);

  /**
   * Angular lifecycle hook called after component initialization.
   * Retrieves the active user account from MSAL service and sets the current user profile and name.
   */
  ngOnInit(): void {
    this.currentUserProfile = this.msalService.instance.getActiveAccount();
    if (this.currentUserProfile) {
      this.currentUserName.set(this.currentUserProfile.name ?? '');
    }
  }

  /**
   * Navigates the user to the home/dashboard page.
   * Uses the router service to redirect to the dashboard route defined in RouteConstants.
   */
  protected redirectHomePage(): void {
    this.routerService.navigate([RouteConstants.Dashboard.Link]);
  }

  /**
   * Opens the navigation drawer/sidebar.
   * Calls the drawer service to display the side navigation menu.
   */
  protected openDrawer(): void {
    this.drawerService.openDrawer();
  }
}
