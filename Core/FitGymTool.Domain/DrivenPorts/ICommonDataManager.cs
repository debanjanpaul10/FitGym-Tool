// *********************************************************************************
//	<copyright file="IFitGymCommonDataService.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The FitGym Common Data Service Interface.</summary>
// *********************************************************************************


using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;

namespace FitGymTool.Domain.DrivenPorts;

/// <summary>
/// The FitGym Common Data Service Interface.
/// </summary>
public interface ICommonDataManager
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>The mapping master data domain.</returns>
	Task<MappingMasterData> GetMappingsMasterDataAsync();

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportData">The bug report data.</param>
	/// <returns>The boolean for success/failure.</returns>
	Task<bool> AddNewBugReportDataAsync(BugReportData bugReportData);

	/// <summary>
	/// Gets the active ai features asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="AIFeature"/></returns>
	Task<IEnumerable<AIFeature>> GetActiveAIFeaturesAsync();

	/// <summary>
	/// Executes the aisql query asynchronous.
	/// </summary>
	/// <param name="aiSqlQuery">The ai SQL query.</param>
	/// <returns>The json format of the sql response.</returns>
	Task<string> ExecuteAISQLQueryAsync(string aiSqlQuery);

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="SampleChatbotPromptsDomain"/></returns>
	Task<IEnumerable<SampleChatbotPromptsDomain>> GetSamplePromptsForChatbotAsync();
}
