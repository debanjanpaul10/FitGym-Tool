import { Routes } from '@angular/router';
import { RouteConstants } from '@shared/application.constants';

export const routes: Routes = [
  {
    path: RouteConstants.Dashboard.Link,
    loadComponent: () =>
      import('./pages/dashboard/dashboard-component').then(
        (c) => c.DashboardComponent
      ),
  },
  {
    path: RouteConstants.Login.Link,
    loadComponent: () =>
      import('./pages/login/login-component').then((c) => c.LoginComponent),
  },
  {
    path: RouteConstants.Error.Link,
    loadComponent: () =>
      import('./pages/error/error-component').then((c) => c.ErrorComponent),
  },
];
