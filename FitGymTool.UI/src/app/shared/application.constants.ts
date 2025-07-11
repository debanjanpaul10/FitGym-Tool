export class RouteConstants {
  public static Dashboard = {
    Name: 'Dashboard',
    Link: 'dashboard',
    RouteValue: '/dashboard',
  };
  public static Login = {
    Name: 'Login',
    Link: '',
    RouteValue: '/',
  };
  public static Error = {
    Name: 'Error',
    Link: 'error',
    RouteValue: '/errpr',
  };
  public static MemberManagement = {
    Name: 'Members',
    Link: 'members',
    RouteValue: '/members',
  };
}

export class LoginPageConstants {
  public static Headings = {
    LoginButtonName: 'Login to FitGym Tool',
  };
}

export class DrawerConstants {
  public static Headings = {
    BrandText: 'FitGym Tool',
    LogoutButton: 'Logout',
  };
}

export class ChartConstants {
  public static Labels = {
    Paid: {
      yAxis: 'Fees paid',
      legend: 'Fees paid',
    },
    NotPaid: {
      yAxis: 'Yet to pay',
      legend: 'Fees yet to be paid',
    },
    SubCancelled: {
      yAxis: 'Cancelled',
      xAxis: 'Subscription Cancelled This Month',
      legend: 'Subscription fees cancelled in the current month',
    },
  };
  public static AxisLabels = {};
  public static Header = 'Current Revenue ';
  public static SubHeader =
    'Revenue generated from the end of the last month till today';
}
