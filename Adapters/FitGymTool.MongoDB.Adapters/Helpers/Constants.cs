// *********************************************************************************
//	<copyright file="Constants.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Constants Class.</summary>
// *********************************************************************************

namespace FitGymTool.MongoDB.Adapters.Helpers;

/// <summary>
/// The Constants class.
/// </summary>
internal static class Constants
{
	/// <summary>
	/// The Configuration Constants.
	/// </summary>
	internal static class ConfigurationConstants
	{
		/// <summary>
		/// The mongo database connection string constant
		/// </summary>
		internal const string MongoDbConnectionStringConstant = "MongoDbConnectionString";
	}

	/// <summary>
	/// The Logging Constants Class.
	/// </summary>
	internal static class LoggingConstants
	{
		/// <summary>
		/// The method started message constant
		/// </summary>
		internal static readonly string MethodStartedMessageConstant = "Method {0} started at {1}";

		/// <summary>
		/// The method ended message constant
		/// </summary>
		internal static readonly string MethodEndedMessageConstant = "Method {0} ended at {1}";

		/// <summary>
		/// The method failed with message constant.
		/// </summary>
		/// <returns>{0} failed at {1} with {2}</returns>
		internal const string MethodFailedWithMessageConstant = "Method {0} failed at {1} with {2}";
	}

	/// <summary>
	/// The MongoDB Constants.
	/// </summary>
	internal static class MongoDBConstants
	{
		/// <summary>
		/// The ai agents knowledge base database
		/// </summary>
		internal const string AiAgentsKnowledgeBaseDatabase = "ai-agents-knowledgebase";

		/// <summary>
		/// The fit gym tool database schema collection
		/// </summary>
		internal const string FitGymToolDatabaseSchemaCollection = "FitGymToolDatabaseSchema";

		/// <summary>
		/// The fit gym tool knowledge base collection
		/// </summary>
		internal const string FitGymToolDatabaseKnowledgeBase = "FitGymToolDatabaseKnowledgeBase";

		/// <summary>
		/// The fit gym tool rag knowledgebase
		/// </summary>
		internal const string FitGymToolRAGKnowledgebase = "FitGymToolRAGKnowledgebase";
	}

	/// <summary>
	/// The Exception Constants class.
	/// </summary>
	internal static class ExceptionConstants
	{
		/// <summary>
		/// Something went wrong message constant
		/// </summary>
		public static readonly string SomethingWentWrongMessageConstant = "Oops! Something went wrong. Please try again after sometime.";
	}
}
