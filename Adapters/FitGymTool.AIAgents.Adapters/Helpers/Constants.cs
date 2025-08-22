// *********************************************************************************
//	<copyright file="Constants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The constants class.</summary>
// *********************************************************************************
namespace FitGymTool.AIAgents.Adapters.Helpers;

/// <summary>
/// The constants class.
/// </summary>
internal static class Constants
{
	/// <summary>
	/// The logging constants.
	/// </summary>
	internal static class LoggingConstants
	{
		/// <summary>
		/// The log helper method start
		/// </summary>
		internal const string LogHelperMethodStart = "{0} started at {1} for {2}";

		/// <summary>
		/// The log helper method ended
		/// </summary>
		internal const string LogHelperMethodEnded = "{0} ended at {1} for {2}";

		/// <summary>
		/// The log helper method failed
		/// </summary>
		internal const string LogHelperMethodFailed = "{0} failed at {1} with message {2}";

		/// <summary>
		/// The ai agents laboratory
		/// </summary>
		internal const string AiAgentsLaboratory = "AIAgentsLaboratory";
	}

	/// <summary>
	/// The configuration constants.
	/// </summary>
	internal static class ConfigurationConstants
	{
		/// <summary>
		/// The bearer constant
		/// </summary>
		internal const string BearerConstant = "Bearer";

		/// <summary>
		/// The ai agents ad client identifier
		/// </summary>
		internal const string AiAgentsAdClientId = "AiAgentsLab:ClientId";

		/// <summary>
		/// The ai agents ad client secret
		/// </summary>
		internal const string AiAgentsAdClientSecret = "AiAgentsLab:ClientSecret";

		/// <summary>
		/// The tenant id constant.
		/// </summary>
		internal const string TenantIdConstant = "AzureAdTenantId";

		/// <summary>
		/// The ibbs ai FICC token audience.
		/// </summary>
		internal const string AiAgentsFICCTokenAudience = "api://AzureADTokenExchange";

		/// <summary>
		/// The token scope format.
		/// </summary>
		internal const string TokenScopeFormat = "{0}/.default";

		/// <summary>
		/// The managed identity client identifier constant
		/// </summary>
		internal const string ManagedIdentityClientIdConstant = "ManagedIdentityClientId";

		/// <summary>
		/// The ai agents web issuer constant
		/// </summary>
		internal const string AiAgentsWebIssuerConstant = "AiAgentsLab:WebIssuer";

		/// <summary>
		/// The is development mode constant
		/// </summary>
		internal const string IsDevelopmentModeConstant = "IsDevelopmentMode";

		/// <summary>
		/// The application json constant
		/// </summary>
		internal const string ApplicationJsonConstant = "application/json";

		/// <summary>
		/// The ai agents HTTP client
		/// </summary>
		internal const string AiAgentsHttpClient = "aiagentsclient";

		/// <summary>
		/// The ai agents API base URL
		/// </summary>
		internal const string AiAgentsApiBaseUrl = "AiAgentsLab:ApiBaseUrl";
	}

	/// <summary>
	/// The Exception Constants class.
	/// </summary>
	internal static class ExceptionConstants
	{
		/// <summary>
		/// Something went wrong message constant
		/// </summary>
		internal const string SomethingWentWrongMessageConstant = "Something went wrong!";

		/// <summary>
		/// The ai services cannot be availed exception constant.
		/// </summary>
		internal const string AiServicesCannotBeAvailedExceptionConstant = "Oops! It seems our AI Services are down as of this moment. Please try again after sometime.";
	}

	/// <summary>
	/// The AI Agents Routes constants.
	/// </summary>
	internal static class AIAgentsRoutesConstants
	{
		/// <summary>
		/// The get bug severity API route
		/// </summary>
		internal const string GetBugSeverity_ApiRoute = "fitgymtoolai/getbugseverity";

		/// <summary>
		/// The get chatbot response API route
		/// </summary>
		internal const string GetChatbotResponse_ApiRoute = "fitgymtoolai/getchatbotresponse";
	}
}
