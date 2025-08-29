using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.MappingData;
using FitGymTool.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static FitGymTool.API.Helpers.APIConstants;
using static FitGymTool.API.Helpers.SwaggerConstants.FitGymCommonController;

namespace FitGymTool.API.Controllers;

/// <summary>
/// The Fit Gym Common Controller Class.
/// </summary>
/// <param name="fitGymCommonHandler">The fit gym common service.</param>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <seealso cref="FitGymTool.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.FitGymCommonApiRoutes.BaseRoute_RoutePrefix)]
public class FitGymCommonController(ICommonHandler fitGymCommonHandler, IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>The response data dto.</returns>
	[HttpGet(RouteConstants.FitGymCommonApiRoutes.GetMappingsMasterData_ApiRoute)]
	[ProducesResponseType(typeof(MappingMasterDataDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = GetMappingsMasterDataAction.Summary, Description = GetMappingsMasterDataAction.Description, OperationId = GetMappingsMasterDataAction.OperationId)]
	public async Task<ResponseDTO> GetMappingsMasterDataAsync()
	{
		if (IsAuthorized())
		{
			var result = await fitGymCommonHandler.GetMappingsMasterDataAsync();
			if (result is not null)
			{
				return HandleSuccessRequestResponse(result);
			}

			return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequestResponse();
	}

	/// <summary>
	/// Adds the bug report data asynchronous.
	/// </summary>
	/// <param name="addBugReportData">The input dto for add new bug data.</param>
	/// <returns>The action result of the response dto.</returns>
	[HttpPost(RouteConstants.FitGymCommonApiRoutes.AddBugReport_ApiRoute)]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = AddBugReportDataAction.Summary, Description = AddBugReportDataAction.Description, OperationId = AddBugReportDataAction.OperationId)]
	public async Task<ResponseDTO> AddBugReportDataAsync([FromBody] AddBugReportDTO addBugReportData)
	{
		if (IsAuthorized())
		{
			addBugReportData.CreatedBy = base.UserEmail;
			var result = await fitGymCommonHandler.AddNewBugReportDataAsync(addBugReportData);
			if (result)
			{
				return HandleSuccessRequestResponse(result);
			}

			return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequestResponse();
	}
}
