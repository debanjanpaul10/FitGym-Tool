// *********************************************************************************
//	<copyright file="SwaggerConstants.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Swagger Constants Class.</summary>
// *********************************************************************************

namespace FitGymTool.API.Helpers;

/// <summary>
/// The Swagger Constants Class.
/// </summary>
internal static class SwaggerConstants
{
    /// <summary>
    /// The Fit Gym Common Controller.
    /// </summary>
    internal static class FitGymCommonController
    {
        /// <summary>
        /// Swagger documentation for GetMappingsMasterDataAsync
        /// </summary>
        internal static class GetMappingsMasterDataAction
        {
            internal const string Summary = "Gets the mappings master data asynchronous.";
            internal const string Description = "Gets the master tables for all the mapping lookup data in a single DTO containing individual lists.";
            internal const string OperationId = nameof(GetMappingsMasterDataAction);
        }

        /// <summary>
        /// Swagger documentation for AddBugReportDataAsync
        /// </summary>
        internal static class AddBugReportDataAction
        {
            internal const string Summary = "Adds the bug report data asynchronous.";
            internal const string Description = "Creates a new bug report documentation by user/member to be reviewed by devs and fixed.";
            internal const string OperationId = nameof(AddBugReportDataAction);
        }
    }

    /// <summary>
    /// The Member Fees Controller.
    /// </summary>
    internal static class MemberFeesController
    {
        /// <summary>
        /// The swagger documentation for GetCurrentMonthFeesAndRevenueStatusAsync
        /// </summary>
        internal static class GetCurrentMonthFeesAndRevenueStatusAction
        {
            internal const string Summary = "Gets the current month fees and revenue status asynchronous.";
            internal const string Description = "For the current month, it will bring the fees that has been paid, due, overdue, etc and calculate the revenue";
            internal const string OperationId = nameof(GetCurrentMonthFeesAndRevenueStatusAction);
        }

        /// <summary>
        /// The swagger documentation for GetCurrentFeesStructureAsync
        /// </summary>
        internal static class GetCurrentFeesStructureAction
        {
            internal const string Summary = "Gets the current fees structure asynchronous.";
            internal const string Description = "Gets the current fees structure for the members in general.";
            internal const string OperationId = nameof(GetCurrentFeesStructureAction);
        }

        /// <summary>
        /// The swagger documentation for GetCurrentMembersFeesStatusAsync
        /// </summary>
        internal static class GetCurrentMembersFeesStatusAction
        {
            internal const string Summary = "Gets the current members fees status asynchronous.";
            internal const string Description = "Gets the fees status for all members for the current date.";
            internal const string OperationId = nameof(GetCurrentMembersFeesStatusAction);
        }

        /// <summary>
        /// The swagger documentation for GetPaymentHistoryDataForMemberAsync
        /// </summary>
        internal static class GetPaymentHistoryDataForMemberAction
        {
            internal const string Summary = "Gets the payment history data for member asynchronous.";
            internal const string Description = "Gets the payment status history for individual member by email id.";
            internal const string OperationId = nameof(GetPaymentHistoryDataForMemberAction);
        }
    }

    /// <summary>
    /// The Members Controller.
    /// </summary>
    internal static class MembersController
    {
        /// <summary>
        /// Swagger documentation for AddNewMemberAsync
        /// </summary>
        internal static class AddNewMemberAction
        {
            internal const string Summary = "Adds a new member to the database asynchronously.";
            internal const string Description = "Creates a new member record in the database with the provided member details and optional admin flag.";
            internal const string OperationId = nameof(AddNewMemberAction);
        }

        /// <summary>
        /// Swagger documentation for GetAllMembersAsync
        /// </summary>
        internal static class GetAllMembersAction
        {
            internal const string Summary = "Gets all members from the database asynchronously.";
            internal const string Description = "Retrieves a complete list of all registered members with their details.";
            internal const string OperationId = nameof(GetAllMembersAction);
        }

        /// <summary>
        /// Swagger documentation for GetMemberByEmailIdAsync
        /// </summary>
        internal static class GetMemberByEmailIdAction
        {
            internal const string Summary = "Gets a single member's details by Member's Email ID asynchronously.";
            internal const string Description = "Retrieves detailed information for a specific member using their email address as the identifier.";
            internal const string OperationId = nameof(GetMemberByEmailIdAction);
        }

        /// <summary>
        /// Swagger documentation for UpdateMemberDetailsAsync
        /// </summary>
        internal static class UpdateMemberDetailsAction
        {
            internal const string Summary = "Updates an existing member's details asynchronously.";
            internal const string Description = "Modifies the details of an existing member record in the database with the provided updated information.";
            internal const string OperationId = nameof(UpdateMemberDetailsAction);
        }

        /// <summary>
        /// Swagger documentation for UpdateMembershipStatusDataAsync
        /// </summary>
        internal static class UpdateMembershipStatusAction
        {
            internal const string Summary = "Updates the membership status data asynchronous.";
            internal const string Description = "Modifies the membership status for a specific member, such as active, inactive, suspended, etc.";
            internal const string OperationId = nameof(UpdateMembershipStatusAction);
        }
    }

}
