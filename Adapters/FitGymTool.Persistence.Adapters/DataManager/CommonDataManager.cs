// *********************************************************************************
//	<copyright file="CommonDataManager.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The FitGym Common Data Manager Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using FitGymTool.Domain.DomainEntities.Mapping;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Persistence.Adapters.Contracts;
using FitGymTool.Persistence.Adapters.Helpers.Constants;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.Json;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Persistence.Adapters.DataManager;

/// <summary>
/// The FitGym Common Data Manager Class.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <seealso cref="ICommonDataManager" />
public class CommonDataManager(IUnitOfWork unitOfWork, ILogger<CommonDataManager> logger) : ICommonDataManager
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>A tupple containing the mapping master data.</returns>
	public async Task<MappingMasterData> GetMappingsMasterDataAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, DatabaseConstants.NotApplicableStringConstant));

			var feesDurationMapping = await unitOfWork.Repository<FeesDurationMapping>().GetAllAsync(x => x.IsActive);
			var feesPaymentStatusMapping = await unitOfWork.Repository<FeesPaymentStatusMapping>().GetAllAsync(x => x.IsActive);
			var membershipStatusMapping = await unitOfWork.Repository<MembershipStatusMapping>().GetAllAsync(x => x.IsActive);
			var bugSeverityMapping = await unitOfWork.Repository<BugSeverityMapping>().GetAllAsync(x => x.IsActive);
			var aIServiceStatusMappings = await unitOfWork.Repository<AIServiceStatusMapping>().GetAllAsync(x => x.IsActive);

			return new MappingMasterData()
			{
				FeesDurationMapping = feesDurationMapping,
				FeesPaymentStatusMapping = feesPaymentStatusMapping,
				MembershipStatusMapping = membershipStatusMapping,
				BugSeverityMapping = bugSeverityMapping,
				AIServiceStatusMappings = aIServiceStatusMappings
			};
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, DatabaseConstants.NotApplicableStringConstant));
		}
	}

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportData">The bug report data.</param>
	/// <returns>The boolean for success/failure.</returns>
	public async Task<bool> AddNewBugReportDataAsync(BugReportData bugReportData)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, bugReportData.CreatedBy));

			var bugStatusEntity = await unitOfWork.Repository<BugItemStatusMapping>().FirstOrDefaultAsync(status => status.StatusName == DatabaseConstants.NotStartedConstant && status.IsActive);
			bugReportData.BugStatusId = bugStatusEntity?.Id ?? 0;

			await unitOfWork.Repository<BugReportData>().AddAsync(bugReportData);
			await unitOfWork.SaveChangesAsync();

			return true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, bugReportData.CreatedBy));
		}
	}

	/// <summary>
	/// Gets the active ai features asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="T:FitGymTool.Domain.DomainEntities.AIEntities.AIFeature" />
	/// </returns>
	public async Task<IEnumerable<AIFeature>> GetActiveAIFeaturesAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetActiveAIFeaturesAsync), DateTime.UtcNow, string.Empty));
			return await unitOfWork.Repository<AIFeature>().GetAllAsync(x => x.IsActive).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetActiveAIFeaturesAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetActiveAIFeaturesAsync), DateTime.UtcNow, string.Empty));
		}
	}

	/// <summary>
	/// Executes the aisql query asynchronous.
	/// </summary>
	/// <param name="aiSqlQuery">The ai SQL query.</param>
	/// <returns>
	/// The json format of the sql response.
	/// </returns>
	public async Task<string> ExecuteAISQLQueryAsync(string aiSqlQuery)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(ExecuteAISQLQueryAsync), DateTime.UtcNow, aiSqlQuery));
			var result = await unitOfWork.ExecuteSqlQueryRawAsync<List<Object>>(aiSqlQuery).ConfigureAwait(false);
			return JsonSerializer.Serialize(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(ExecuteAISQLQueryAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(ExecuteAISQLQueryAsync), DateTime.UtcNow, aiSqlQuery));
		}
	}

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="T:FitGymTool.Domain.DomainEntities.AIEntities.SampleChatbotPromptsDomain" />
	/// </returns>
	public async Task<IEnumerable<SampleChatbotPromptsDomain>> GetSamplePromptsForChatbotAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
			return await unitOfWork.Repository<SampleChatbotPromptsDomain>().GetAllAsync(x => x.IsActive).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
		}
	}
}
