// *********************************************************************************
//	<copyright file="APIConstants.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The API constants Class.</summary>
// *********************************************************************************

namespace FitGymTool.API.Helpers;

/// <summary>
/// The API constants Class.
/// </summary>
public class APIConstants
{
	/// <summary>
	/// The Swagger constants class contains constants related to Swagger documentation.
	/// </summary>
	public static class SwaggerConstants
	{
		/// <summary>
		/// The api version for Swagger documentation.
		/// </summary>
		public const string ApiVersion = "v1";

		/// <summary>
		/// The swagger endpoint for the API documentation.
		/// </summary>
		public const string SwaggerEndpointUrl = "/swagger/v1/swagger.json";

		/// <summary>
		/// The swagger ui endpoint prefix.
		/// </summary>
		public const string SwaggerUiPrefix = "swaggerui";

		/// <summary>
		/// The description for the Swagger documentation.
		/// </summary>
		public const string SwaggerDescription = "API documentation for FitGym Tool";

		/// <summary>
		/// The Author Details class contains information about the author of the API.
		/// </summary>
		public static class AuthorDetails
		{
			/// <summary>
			/// The author's name.
			/// </summary>
			public static readonly string Name = "Debanjan Paul";

			/// <summary>
			/// The author's email address.
			/// </summary>
			public static readonly string Email = "debanjanpaul10@gmail.com";
		}

		/// <summary>
		/// The API name for Swagger documentation.
		/// </summary>
		public const string ApplicationAPIName = "FitGymTool.API";
	}

	/// <summary>
	/// The Authentication constants class contains constants related to authentication settings.
	/// </summary>
	public static class AuthenticationConstants
	{
		/// <summary>
		/// The Azure AD API Client ID constant.
		/// </summary>
		public const string AzureAdApiClientIdConstant = "AzureAdApiClientId";

		/// <summary>
		/// The Azure AD API Tenant ID constant.
		/// </summary>
		public const string ApiTenantIdConstant = "AzureAdApiTenantId";

		/// <summary>
		/// The Azure AD API Issuer constant.
		/// </summary>
		public const string AzureAdApiIssuerConstant = "AzureAdApiIssuer";

		/// <summary>
		/// The user full name claim constant.
		/// </summary>
		public const string UserFullNameClaimConstant = "name";

		/// <summary>
		/// The user email claim constant.
		/// </summary>
		public const string UserEmailClaimConstant = "preferred_username";
	}

	/// <summary>
	/// The Logging Constants Class.
	/// </summary>
	public static class LoggingConstants
	{
		/// <summary>
		/// The method started message constant
		/// </summary>
		public static readonly string MethodStartedMessageConstant = "Method {0} started at {1} for {2}";

		/// <summary>
		/// The method ended message constant
		/// </summary>
		public static readonly string MethodEndedMessageConstant = "Method {0} ended at {1} for {2}";

		/// <summary>
		/// The method failed with message constant.
		/// </summary>
		/// <returns>{0} failed at {1} with {2}</returns>
		public const string MethodFailedWithMessageConstant = "Method {0} failed at {1} with {2}";
	}

	/// <summary>
	/// The Configuration Constants class.
	/// </summary>
	public static class ConfigurationConstants
	{
		/// <summary>
		/// The development app settings file name.
		/// </summary>
		public const string DevelopmentAppSettingsFile = "appsettings.Development.json";

		/// <summary>
		/// The managed identity client ID key constant.
		/// </summary>
		public const string ManagedIdentityClientIdConstant = "ManagedIdentityClientId";

		/// <summary>
		/// The app configuration endpoint key constant.
		/// </summary>
		public const string AppConfigurationEndpointKeyConstant = "AppConfigurationEndpoint";

		/// <summary>
		/// The base configuration app config key constant.
		/// </summary>
		public const string BaseConfigurationAppConfigKeyConstant = "BaseConfiguration";

		/// <summary>
		/// The FitGym API app config key constant.
		/// </summary>
		public const string FitGymAPIAppConfigKeyConstant = "FitGym.API";
	}

	/// <summary>
	/// The Header Constants class.
	/// </summary>
	public static class HeaderConstants
	{
		/// <summary>
		/// The constant for the Not Applicable string value.
		/// </summary>
		public const string NotApplicableStringConstant = "NA";
	}

	/// <summary>
	/// The exception constants class.
	/// </summary>
	public static class ExceptionConstants
	{
		/// <summary>
		/// he Unauthorized Access Message Constant.
		/// </summary>
		public const string UnauthorizedAccessMessageConstant = "Unauthorized access. Please log in to continue.";

		/// <summary>
		/// Something went wrong message constant
		/// </summary>
		public const string SomethingWentWrongMessageConstant = "Something went wrong. Please try again later.";

		/// <summary>
		/// The configuration value is empty message constant.
		/// </summary>
		public const string ConfigurationValueIsEmptyMessageConstant = "The configuration key is empty!";

		/// <summary>
		/// The invalid token exception constant.
		/// </summary>
		public const string InvalidTokenExceptionConstant = "Invalid token: Identity is not authenticated.";
	}

	/// <summary>
	/// The Validation Error Messages Class.
	/// </summary>
	public static class ValidationErrorMessages
	{
		/// <summary>
		/// The member details cannot be null message constant.
		/// </summary>
		public const string MemberDetailsCannotBeNull = "Member details cannot be null.";

		/// <summary>
		/// The member already exists message constant.
		/// </summary>
		public const string MemberAlreadyExistsMessageConstant = "Member with the same ID and GUID already exists.";

		/// <summary>
		/// The member not found message constant.
		/// </summary>
		public const string MemberNotFoundMessageConstant = "Member with the specified ID was not found.";

		/// <summary>
		/// The member details are invalid message constant.
		/// </summary>
		public const string MemberCouldNotBeAddedMessageConstant = "Member could not be added. Please check the details and try again.";

	}
}
