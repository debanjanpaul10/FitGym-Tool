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

import { DrawerService } from '@services/drawer.service';
import { DrawerConstants, RouteConstants } from '@shared/application.constants';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-component',
  imports: [AvatarModule, CommonModule, ButtonModule, TooltipModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  public BrandText = DrawerConstants.Headings.BrandText;
  public currentUserProfile: AccountInfo | null = null;
  public currentUserName: WritableSignal<string> = signal('');

  private readonly msalService: MsalService = inject(MsalService);
  private readonly drawerService: DrawerService = inject(DrawerService);
  private readonly routerService: Router = inject(Router);

  ngOnInit(): void {
    this.currentUserProfile = this.msalService.instance.getActiveAccount();
    if (this.currentUserProfile) {
      this.currentUserName.set(this.currentUserProfile.name ?? '');
    }
  }

  public redirectHomePage(): void {
    this.routerService.navigate([RouteConstants.Dashboard.Link]);
  }

  public openDrawer(): void {
    this.drawerService.openDrawer();
  }
}
