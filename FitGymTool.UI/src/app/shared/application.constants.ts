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
    RouteValue: '/error',
  };
  public static MemberManagement = {
    Name: 'Members',
    Link: 'members',
    RouteValue: '/members',
  };
  public static FeesManagement = {
    Name: 'Fees',
    Link: 'fees',
    RouteValue: '/fees',
  };
  public static FacilityManagement = {
    Name: 'Facility',
    Link: 'facility',
    RouteValue: '/facility',
  };
}

export class ToasterSuccessMessages {
  public static MemberManagement = {
    AddMemberSuccess: 'Member Added Successfully',
  };
  public static Common = {
    BugReportSubmitSuccess: 'The Bug report has been successfully submitted',
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
  public static RevenueChartConstants = {
    Labels: {
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
    },
    Header: 'Current Revenue',
    SubHeader: 'Revenue generated from the end of the last month till today',
  };

  public static ActiveUsersChartConstants = {
    Labels: {
      Active: {
        yAxis: 'Active memberships',
        legend: 'Active',
      },
      OnTermination: {
        yAxis: 'On termination memberships',
        legend: 'On Termination',
      },
      Expired: {
        yAxis: 'Expired memberships',
        legend: 'Expired',
      },
    },
    Header: 'Active Members',
    SubHeader: 'Current status of the Gym Members',
  };
}

export class MemberManagementConstants {
  public static MembersDashboardConstants = {
    AddMember: {
      Name: 'Add a new member',
      ImageSrc: '../../../assets/Images/add-user.jpg',
      Alt: 'Add member image',
    },
    TerminateMember: {
      Name: 'Terminate an existing member',
      ImageSrc: '../../../assets/Images/terminate-user.jpg',
      Alt: 'Terminate a member image',
    },
    UpdateMember: {
      Name: 'Update an existing member',
      ImageSrc: '../../../assets/Images/edit-user.jpg',
      Alt: 'Update member image',
    },
  };
  public static AddNewMemberConstants = {
    genderOptions: [
      { label: 'Male', value: 'Male' },
      { label: 'Female', value: 'Female' },
      { label: 'Other', value: 'Other' },
    ],
    membershipStatusOptions: [
      { label: 'Active', value: 'Active' },
      { label: 'Inactive', value: 'Inactive' },
      { label: 'Suspended', value: 'Suspended' },
      { label: 'Pending', value: 'Pending' },
    ],
    Header: 'Add a new member',
  };
}

export class CommonApplicationConstants {
  public static BugReportConstants = {
    Header: 'File a bug report',
  };
}
