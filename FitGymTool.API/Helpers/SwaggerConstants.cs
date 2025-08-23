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

		/// <summary>
		/// The swagger documentation for UpdateMemberFeesDataAsync
		/// </summary>
		internal static class UpdateMemberFeesDataAction
		{
			internal const string Summary = "Updates the fees payment status for member asynchronous.";
			internal const string Description = "Updates the fees payment status for member for time period and member alias asynchronous.";
			internal const string OperationId = nameof(UpdateMemberFeesDataAction);
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

		/// <summary>
		/// Swagger documentation for AddNewMemberFeesPaymentDurationAsync
		/// </summary>
		internal static class AddNewMemberFeesPaymentDurationAction
		{
			internal const string Summary = "Adds fees payment and subscription duration for new member asynchronous.";
			internal const string Description = "Adds the fees payment and subscription duration data for new member in mapping table.";
			internal const string OperationId = nameof(AddNewMemberFeesPaymentDurationAction);
		}

	}

	/// <summary>
	/// The AI Services Controller.
	/// </summary>
	internal static class AIServicesController
	{
		/// <summary>
		/// Swagger documentation for RespondAsync.
		/// </summary>
		internal static class RespondAction
		{
			internal const string Summary = "Responds to user query asynchronously.";
			internal const string Description = "Calls the AI service to handle the user query.";
			internal const string OperationId = nameof(RespondAction);
		}

		/// <summary>
		/// Swagger documentation for GetBugSeverityStatusAsync.
		/// </summary>
		internal static class GetBugSeverityStatusAction
		{
			internal const string Summary = "Gets the bug severity status.";
			internal const string Description = "Gets the bug severity status for the user's mentioned bug using AI services.";
			internal const string OperationId = nameof(GetBugSeverityStatusAction);
		}

		/// <summary>
		/// Swagger documentation for GetActiveAIFeaturesAsync.
		/// </summary>
		internal static class GetActiveAIFeaturesAction
		{
			internal const string Summary = "Gets the active AI features.";
			internal const string Description = "Gets the list of active AI features for FitGym tool along with their service statuses.";
			internal const string OperationId = nameof(GetActiveAIFeaturesAction);
		}

		/// <summary>
		/// Swagger documentation for GetDatabaseSchemaJsonAsync
		/// </summary>
		internal static class GetDatabaseSchemaJsonAction
		{
			internal const string Summary = "Gets the database schema json.";
			internal const string Description = "Gets the detailed database schema json file.";
			internal const string OperationId = nameof(GetDatabaseSchemaJsonAction);
		}

		/// <summary>
		/// Swagger documentation for GetDatabaseKnowledgeBaseJsonAsync
		/// </summary>
		internal static class GetDatabaseKnowledgeBaseJsonAction
		{
			internal const string Summary = "Gets the database knowledge base json.";
			internal const string Description = "Gets the detailed database knowledge base json file.";
			internal const string OperationId = nameof(GetDatabaseSchemaJsonAction);
		}

		/// <summary>
		/// Swagger documentation for GetSamplePromptsForChatbotAsync
		/// </summary>
		internal static class GetSamplePromptsForChatbotAction
		{
			internal const string Summary = "Gets a list of sample prompts for ai chatbot.";
			internal const string Description = "Gets the list of sample prompts for ai chatbot that can be executed by user.";
			internal const string OperationId = nameof(GetSamplePromptsForChatbotAction);
		}
	}
}
