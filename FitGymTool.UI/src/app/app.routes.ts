import { Routes } from '@angular/router';
import { RouteConstants } from '@shared/application.constants';

export const routes: Routes = [
  {
    path: RouteConstants.Dashboard.Link,
    loadComponent: () =>
      import('./pages/dashboard/dashboard').then((c) => c.Dashboard),
  },
  {
    path: RouteConstants.Login.Link,
    loadComponent: () => import('./pages/login/login').then((c) => c.Login),
  },
  {
    path: RouteConstants.Error.Link,
    loadComponent: () => import('./pages/error/error').then((c) => c.Error),
  },
];
