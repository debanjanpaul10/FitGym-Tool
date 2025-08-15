// *********************************************************************************
//	<copyright file="TokenHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Token helper class.</summary>
// *********************************************************************************

using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using static FitGymTool.AIAgents.Adapters.Helpers.Constants;

namespace FitGymTool.AIAgents.Adapters.Utilities;

/// <summary>
/// The Token Helper Class.
/// </summary>
[ExcludeFromCodeCoverage]
internal class TokenHelper
{
	/// <summary>
	/// Gets the ai agents lab token asynchronous.
	/// </summary>
	/// <param name="configuration">The configuration.</param>
	/// <param name="logger">The logger.</param>
	/// <returns>The token value.</returns>
	/// <exception cref="System.Exception"></exception>
	public static async Task<string> GetAiAgentsLabTokenAsync(IConfiguration configuration, ILogger logger)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAiAgentsLabTokenAsync), DateTime.UtcNow, LoggingConstants.AiAgentsLaboratory));

			var tenantId = configuration[ConfigurationConstants.TenantIdConstant];
			var clientId = configuration[ConfigurationConstants.AiAgentsAdClientId];
			var clientSecret = configuration[ConfigurationConstants.AiAgentsAdClientSecret];
			var scopes = new[] { string.Format(CultureInfo.CurrentCulture, ConfigurationConstants.TokenScopeFormat, clientId) };

			var isDevelopmentMode = bool.TryParse(configuration[ConfigurationConstants.IsDevelopmentModeConstant], out var parsedValue) && parsedValue;
			if (isDevelopmentMode)
			{
				var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
				var accessToken = await credential.GetTokenAsync(new TokenRequestContext(scopes), CancellationToken.None).ConfigureAwait(false);

				if (string.IsNullOrEmpty(accessToken.Token))
				{
					throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
				}

				return accessToken.Token;
			}
			else
			{
				var miClientId = configuration[ConfigurationConstants.ManagedIdentityClientIdConstant];
				ArgumentException.ThrowIfNullOrEmpty(miClientId);

				var msal = ConfidentialClientApplicationBuilder.Create(clientId)
					.WithClientAssertion((AssertionRequestOptions options) => GetManagedIdentityToken(miClientId, ConfigurationConstants.AiAgentsFICCTokenAudience))
					.WithAuthority(configuration[ConfigurationConstants.AiAgentsWebIssuerConstant]).Build();

				var result = await msal.AcquireTokenForClient(scopes).ExecuteAsync();
				return result.AccessToken;
			}
		}
		catch (Exception ex)
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAiAgentsLabTokenAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAiAgentsLabTokenAsync), DateTime.UtcNow, LoggingConstants.AiAgentsLaboratory));
		}
	}

	/// <summary>
	/// Gets the managed identity token.
	/// </summary>
	/// <param name="msiClientId">The MSI Client Id.</param>
	/// <param name="audience">The FICC audience</param>
	/// <returns>The token data.</returns>
	public static async Task<string> GetManagedIdentityToken(string msiClientId, string audience)
	{
		var miIdentity = Microsoft.Identity.Client.AppConfig.ManagedIdentityId.WithUserAssignedClientId(msiClientId);
		var miApplication = ManagedIdentityApplicationBuilder.Create(miIdentity).Build();
		var miResult = await miApplication.AcquireTokenForManagedIdentity($"{audience}/.default").ExecuteAsync().ConfigureAwait(false);
		return miResult.AccessToken;
	}
}
