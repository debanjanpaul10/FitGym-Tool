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

@Component({
  selector: 'app-left-navigation-component',
  imports: [DrawerModule, ButtonModule, CommonModule],
  templateUrl: './left-navigation-component.html',
  styleUrl: './left-navigation-component.scss',
})
export class LeftNavigationComponent implements OnInit {
  @ViewChild('drawerRef') drawerRef!: Drawer;

  public DrawerConstants = DrawerConstants;
  public isVisible: WritableSignal<boolean> = signal(false);
  public menuItems: any;

  private readonly authService = inject(MsalService);

  ngOnInit(): void {
    this.menuItems = [
      {
        name: 'Add new user',
        icon: 'pi pi-user-plus',
        onClick: () => this.addNewUser(),
      },
      {
        name: 'Logout',
        icon: 'pi pi-sign-out',
        onClick: () => this.logoutRedirect(),
      },
    ];
  }

  public toggleVisible(): void {
    this.isVisible.set(!this.isVisible());
  }

  public closeCallback(e: any): void {
    this.drawerRef.close(e);
  }

  private logoutRedirect(): void {
    this.authService.logoutRedirect();
  }

  private addNewUser(): void {
    alert('Feature being worked on');
  }
}
