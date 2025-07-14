export class ApiRoutes {
  public static MembersApi = {
    BaseRoute: 'api/Members/',
    AddMember_ApiRoute: 'AddMember/',
    GetAllMembers_ApiRoute: 'GetAllMembers',
    GetMemberByEmailId_ApiRoute: 'GetMemberByEmailId',
    UpdateMember_ApiRoute: 'UpdateMember',
    DeleteMember_ApiRoute: 'DeleteMember',
  };
  public static CommonApi = {
    BaseRoute: 'api/Common/',
    GetMappingsMasterData_ApiRoute: 'GetMappingsMasterData',
  };
  public static MemberFeesApi = {
    BaseRoute: 'api/MemberFees/',
    GetCurrentMonthFeesAndRevenueStatus_ApiRoute:
      'GetCurrentMonthFeesAndRevenueStatus',
  };
}
