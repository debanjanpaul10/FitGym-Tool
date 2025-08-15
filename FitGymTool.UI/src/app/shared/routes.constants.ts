export class ApiRoutes {
  public static MembersApi = {
    BaseRoute: 'api/Members/',
    AddMember_ApiRoute: 'AddMember/',
    GetAllMembers_ApiRoute: 'GetAllMembers',
    GetMemberByEmailId_ApiRoute: 'GetMemberByEmailId?memberEmailId=',
    UpdateMemberDetails_ApiRoute: 'UpdateMemberDetails',
    UpdateMembershipDetails_ApiRoute: 'UpdateMembershipStatus',
    AddMemberFeesPaymentDuration_ApiRoute: 'AddMemberFeesPaymentDuration',
  };
  public static CommonApi = {
    BaseRoute: 'api/Common/',
    GetMappingsMasterData_ApiRoute: 'GetMappingsMasterData',
    AddBugReport_ApiRoute: 'AddBugReport',
    GetAgentStatus_ApiRoute:
      'https://app-webapi-ai-agents-lab.azurewebsites.net/hubs/agent-status',
    GetBugSeverityStatus_ApiRoute: 'GetBugSeverityStatus',
    GetActiveAIFeatures_ApiRoute: 'GetActiveAIFeatures',
  };
  public static MemberFeesApi = {
    BaseRoute: 'api/MemberFees/',
    GetCurrentMonthFeesAndRevenueStatus_ApiRoute:
      'GetCurrentMonthFeesAndRevenueStatus',
    GetCurrentFeesStructure_ApiRoute: 'GetCurrentFeesStructure',
    GetCurrentMembersFeesStatus_ApiRoute: 'GetCurrentMembersFeesStatus',
    GetPaymentHistoryDataForMember_ApiRoute:
      'GetPaymentHistoryDataForMember?emailId=',
    UpdateMemberFeesData_ApiRoute: 'UpdateMemberFeesData',
  };
}

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
