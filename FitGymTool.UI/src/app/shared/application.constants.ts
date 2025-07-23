export class ConfigurationConstants {
  public static AuthenticatorConstants = {
    GraphApiUrl:
      'https://graph.microsoft.com/v1.0/users?$filter=startswith(displayName',
    InvalidGrantConstant: 'invalid_grant',
    InvalidGrantError: 'AADSTS65001',
    RedirectError: 'Redirect Error',
  };
}

export class ToasterSuccessMessages {
  public static MemberManagement = {
    AddMemberSuccess: 'Member Added Successfully',
    MembershipStatusUpdatedSuccess:
      'Membership status has been updated successfully',
    UpdateMemberSuccess: 'Member details have been update successfully',
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

  public static CurrentMemberFeesStatusChartConstants = {
    Header: 'Current Member Fees Status',
    SubHeader: 'Current status of the fees payment status for current members',
  };
}

export class MemberManagementConstants {
  public static MembersDashboardConstants = {
    AddMember: {
      Name: 'Add a new member',
      ImageSrc: '../../../assets/Images/add-user.jpg',
      Alt: 'Add member image',
    },
    UpdateMemberDetails: {
      Name: 'Update an existing member details',
      ImageSrc: '../../../assets/Images/edit-user.jpg',
      Alt: 'Update member image',
    },
    UpdateMembershipStatus: {
      Name: 'Update Membership status of member',
      ImageSrc: '../../../assets/Images/member-status-update.jpg',
      Alt: 'Update membership status image',
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

  public static UpdateMembershipStatusConstants = {
    Header: 'Update the membership status',
  };

  public static UpdateMemberDetailsConstants = {
    Header: 'Update the member details',
  };
}

export class CommonApplicationConstants {
  public static BugReportConstants = {
    Header: 'File a bug report',
    InformationMessage:
      'Due to the application being in its initial phases, all bugs serverity will be defaulted to Medium irrespective of your choice',
  };
}

export class FeesManagementConstants {
  public static CurrentFeesStructureConstants = {
    Header: 'Current Fees Structure',
    SubHeader: `The fees structure as of ${new Date().toLocaleDateString()}`,
  };
}
