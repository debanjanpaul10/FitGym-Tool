namespace FitGymTool.API.Adapters.Models.Request;

/// <summary>
/// The Update Member Fees Data DTO.
/// </summary>
public class UpdateMemberFeesDTO
{
	/// <summary>
	/// Gets or sets the member email address.
	/// </summary>
	/// <value>
	/// The member email address.
	/// </value>
	public string MemberEmailAddress { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the amount.
	/// </summary>
	/// <value>
	/// The amount.
	/// </value>
	public decimal Amount { get; set; }

	/// <summary>
	/// Gets or sets from date.
	/// </summary>
	/// <value>
	/// From date.
	/// </value>
	public DateTime FromDate { get; set; }

	/// <summary>
	/// Converts to date.
	/// </summary>
	/// <value>
	/// To date.
	/// </value>
	public DateTime ToDate { get; set; }
}
