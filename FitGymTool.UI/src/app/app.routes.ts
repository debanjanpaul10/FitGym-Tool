import { Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { RouteConstants } from '@shared/routes.constants';

export const routes: Routes = [
  {
    path: RouteConstants.Login.Link,
    loadComponent: () =>
      import('./pages/login/login.component').then((c) => c.LoginComponent),
    canActivate: [MsalGuard],
  },
  {
    path: RouteConstants.Dashboard.Link,
    loadComponent: () =>
      import('./pages/dashboard/dashboard.component').then(
        (c) => c.DashboardComponent
      ),
    canActivate: [MsalGuard],
  },
  {
    path: RouteConstants.MemberManagement.Link,
    loadComponent: () =>
      import('./pages/member-management/member-management.component').then(
        (c) => c.MemberManagementComponent
      ),
    canActivate: [MsalGuard],
  },
  {
    path: RouteConstants.FeesManagement.Link,
    loadComponent: () =>
      import('./pages/fees-management/fees-management.component').then(
        (c) => c.FeesManagementComponent
      ),
    canActivate: [MsalGuard],
  },
  {
    path: RouteConstants.FacilityManagement.Link,
    loadComponent: () =>
      import('./pages/facility-management/facility-management.component').then(
        (c) => c.FacilityManagementComponent
      ),
    canActivate: [MsalGuard],
  },
  {
    path: RouteConstants.Error.Link,
    loadComponent: () =>
      import('./pages/error/error.component').then((c) => c.ErrorComponent),
  },
  {
    path: '**',
    redirectTo: RouteConstants.Error.Link,
  },
];
