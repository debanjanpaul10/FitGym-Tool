import { Routes } from '@angular/router';
import { RouteConstants } from '@shared/routes.constants';

export const routes: Routes = [
  {
    path: RouteConstants.Dashboard.Link,
    loadComponent: () =>
      import('./pages/dashboard/dashboard.component').then(
        (c) => c.DashboardComponent
      ),
  },
  {
    path: RouteConstants.Login.Link,
    loadComponent: () =>
      import('./pages/login/login.component').then((c) => c.LoginComponent),
  },
  {
    path: RouteConstants.Error.Link,
    loadComponent: () =>
      import('./pages/error/error.component').then((c) => c.ErrorComponent),
  },
  {
    path: RouteConstants.MemberManagement.Link,
    loadComponent: () =>
      import('./pages/member-management/member-management.component').then(
        (c) => c.MemberManagementComponent
      ),
  },
  {
    path: RouteConstants.FeesManagement.Link,
    loadComponent: () =>
      import('./pages/fees-management/fees-management.component').then(
        (c) => c.FeesManagementComponent
      ),
  },
  {
    path: RouteConstants.FacilityManagement.Link,
    loadComponent: () =>
      import('./pages/facility-management/facility-management.component').then(
        (c) => c.FacilityManagementComponent
      ),
  },
  {
    path: '**',
    redirectTo: RouteConstants.Error.Link,
  },
];
