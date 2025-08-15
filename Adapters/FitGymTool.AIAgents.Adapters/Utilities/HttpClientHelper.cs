// *********************************************************************************
//	<copyright file="HttpClientHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Http Client Helper Services Class.</summary>
// *********************************************************************************

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static FitGymTool.AIAgents.Adapters.Helpers.Constants;

namespace FitGymTool.AIAgents.Adapters.Utilities;

/// <summary>
/// Http client helper interface.
/// </summary>
public interface IHttpClientHelper
{
	/// <summary>
	/// Gets the ai response asynchronous.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="data">The data.</param>
	/// <param name="apiUrl">The API URL.</param>
	/// <returns>The http response message.</returns>
	Task<HttpResponseMessage> GetAIResponseAsync<T>(T data, string apiUrl);
}

/// <summary>
/// The Http Client Helper.
/// </summary>
/// <param name="configuration">The configuration.</param>
/// <param name="httpClientFactory">The http client factory.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="FitGymTool.AIAgents.Adapters.Utilities.IHttpClientHelper" />
public class HttpClientHelper(ILogger<HttpClientHelper> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory) : IHttpClientHelper
{
	/// <summary>
	/// Gets the ai response asynchronous.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="data">The data.</param>
	/// <param name="apiUrl">The API URL.</param>
	/// <returns>
	/// The http response message.
	/// </returns>
	public async Task<HttpResponseMessage> GetAIResponseAsync<T>(T data, string apiUrl)
	{
		try
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAIResponseAsync), DateTime.UtcNow, data?.GetType().Name ?? string.Empty));
			
			var client = httpClientFactory.CreateClient(ConfigurationConstants.AiAgentsHttpClient);
			ArgumentException.ThrowIfNullOrWhiteSpace(apiUrl);
			await PrepareHttpClientFactoryAsync(client, TokenHelper.GetAiAgentsLabTokenAsync(configuration, logger));

			var inputJson = JsonConvert.SerializeObject(data);
			var contentData = new StringContent(content: inputJson, encoding: Encoding.UTF8, ConfigurationConstants.ApplicationJsonConstant);

			var response = await client.PostAsync(apiUrl, contentData).ConfigureAwait(false);
			if (!response.IsSuccessStatusCode)
			{
				return response.EnsureSuccessStatusCode();
			}

			return response;
		}
		catch (Exception ex)
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAIResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAIResponseAsync), DateTime.UtcNow, data?.GetType().Name ?? string.Empty));
		}
	}

	#region PRIVATE METHODS

	/// <summary>
	/// Prepares http client factory async.
	/// </summary>
	/// <param name="client">The client.</param>
	/// <param name="tokenTask">The task to get the token.</param>
	private static async Task PrepareHttpClientFactoryAsync(HttpClient client, Task<string> tokenTask)
	{
		var token = await tokenTask.ConfigureAwait(false);
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ConfigurationConstants.BearerConstant, token);
	}

	#endregion
}
