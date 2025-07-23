using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGymTool.API.Adapters.Models.Response;

/// <summary>
/// The Fees Structure DTO.
/// </summary>
public class FeesStructureDTO
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the fees duration identifier.
	/// </summary>
	/// <value>
	/// The fees duration identifier.
	/// </value>
	public string FeesDuration { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the fees amount.
	/// </summary>
	/// <value>
	/// The fees amount.
	/// </value>
	public decimal FeesAmount { get; set; }
}
