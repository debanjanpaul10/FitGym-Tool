// *********************************************************************************************************************
//	<copyright file="ConfigurationConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Configuration Constants class contains constants for configuration settings.</summary>
// *********************************************************************************************************************

namespace FitGymTool.Shared.Constants;

/// <summary>
/// The Configuration Constants class contains constants for configuration settings.
/// </summary>
public static class ConfigurationConstants
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
	}

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

	/// <summary>
	/// The user name claim constant.
	/// </summary>
	public const string UserNameClaimConstant = "username";

	/// <summary>
	/// The local SQL database connection string constant.
	/// </summary>
	public const string LocalSqlConnectionStringConstant = "LocalSqlConnectionString";

	/// <summary>
	/// The Azure SQL database connection string constant.
	/// </summary>
	public const string AzureSqlConnectionStringConstant = "AzureSqlConnectionString";
}
