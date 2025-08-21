// *********************************************************************************
//	<copyright file="IAIServicesManager.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The AI services manager interface.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities.AIEntities;

namespace FitGymTool.Domain.DrivenPorts;

/// <summary>
/// The AI services manager interface.
/// </summary>
public interface IAIServicesManager
{
	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>The bug severity response.</returns>
	Task<BugSeverityResponse> GetBugSeverityFromAIServiceAsync(BugSeverityInput bugSeverityInput);

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <returns>The ai agent response.</returns>
	Task<string> GetChatbotResponseAsync(UserQueryRequest userQueryRequest);
}
