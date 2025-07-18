export class ApiRoutes {
  public static MembersApi = {
    BaseRoute: 'api/Members/',
    AddMember_ApiRoute: 'AddMember/',
    GetAllMembers_ApiRoute: 'GetAllMembers',
    GetMemberByEmailId_ApiRoute: 'GetMemberByEmailId',
    UpdateMemberDetails_ApiRoute: 'UpdateMemberDetails',
    UpdateMembershipDetails_ApiRoute: 'UpdateMembershipStatus',
  };
  public static CommonApi = {
    BaseRoute: 'api/Common/',
    GetMappingsMasterData_ApiRoute: 'GetMappingsMasterData',
    AddBugReport_ApiRoute: 'AddBugReport',
  };
  public static MemberFeesApi = {
    BaseRoute: 'api/MemberFees/',
    GetCurrentMonthFeesAndRevenueStatus_ApiRoute:
      'GetCurrentMonthFeesAndRevenueStatus',
  };
}
