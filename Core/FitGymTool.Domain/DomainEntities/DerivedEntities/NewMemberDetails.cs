using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGymTool.Domain.DomainEntities.DerivedEntities;

/// <summary>
/// The New Member Details Domain Entity.
/// </summary>
/// <seealso cref="FitGymTool.Domain.DomainEntities.MemberDetails" />
public class NewMemberDetails : MemberDetails
{
	/// <summary>
	/// Gets or sets the name of the fees duration type.
	/// </summary>
	/// <value>
	/// The name of the fees duration type.
	/// </value>
	public string FeesDurationTypeName { get; set; } = string.Empty;
}
