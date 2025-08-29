using FitGymTool.Domain.DomainEntities.MetadataEntities;
using FitGymTool.Domain.DrivenPorts;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;
using static FitGymTool.MongoDB.Adapters.Helpers.Constants;

namespace FitGymTool.MongoDB.Adapters.MongoDbManager;

/// <summary>
/// The Mongo DB Database Manager.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="mongoClient">The mongo db client.</param>
/// <seealso cref="FitGymTool.Domain.DrivenPorts.IMongoDbDatabaseManager" />
public class MongoDbDatabaseManager(IMongoClient mongoClient, ILogger<MongoDbDatabaseManager> logger) : IMongoDbDatabaseManager
{
	/// <summary>
	/// The ai agents knowledge mongo database
	/// </summary>
	private readonly IMongoDatabase _aiAgentsKnowledgeMongoDatabase = mongoClient.GetDatabase(MongoDBConstants.AiAgentsKnowledgeBaseDatabase);

	/// <summary>
	/// Gets the database knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>
	/// The database knowledge base domain.
	/// </returns>
	public async Task<DatabaseKnowledgeBaseDomain> GetDatabaseKnowledgePiecesJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetDatabaseKnowledgePiecesJsonAsync), DateTime.UtcNow));

			var knowledgePieces = _aiAgentsKnowledgeMongoDatabase.GetCollection<DatabaseKnowledgeBaseDomain>(MongoDBConstants.FitGymToolDatabaseKnowledgeBase);
			if (knowledgePieces is not null)
			{
				return await knowledgePieces.Find(_ => true).FirstAsync().ConfigureAwait(false);
			}

			throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetDatabaseKnowledgePiecesJsonAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetDatabaseKnowledgePiecesJsonAsync), DateTime.UtcNow));
		}
	}

	/// <summary>
	/// Gets the database schema json asynchronous.
	/// </summary>
	/// <returns>
	/// The database schema domain.
	/// </returns>
	public async Task<DatabaseSchemaDomain> GetDatabaseSchemaJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow));

			var dbSchema = _aiAgentsKnowledgeMongoDatabase.GetCollection<TableSchemaDomain>(MongoDBConstants.FitGymToolDatabaseSchemaCollection);
			if (dbSchema is not null)
			{
				var dbSchemaData = await dbSchema.Find(new BsonDocument()).ToListAsync().ConfigureAwait(false);
				return new DatabaseSchemaDomain
				{
					Name = MongoDBConstants.FitGymToolDatabaseSchemaCollection,
					Value = dbSchemaData,
				};
			}

			throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow));
		}
	}

	/// <summary>
	/// Gets the rag knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>
	/// The RAG knowledge base domain.
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<RAGKnowledgeBaseDomain> GetRAGKnowledgePiecesJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetRAGKnowledgePiecesJsonAsync), DateTime.UtcNow));

			var knowledgePieces = _aiAgentsKnowledgeMongoDatabase.GetCollection<RAGKnowledgeBaseDomain>(MongoDBConstants.FitGymToolRAGKnowledgebase);
			if (knowledgePieces is not null)
			{
				return await knowledgePieces.Find(_ => true).FirstAsync().ConfigureAwait(false);
			}

			throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetRAGKnowledgePiecesJsonAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetRAGKnowledgePiecesJsonAsync), DateTime.UtcNow));
		}
	}
}
