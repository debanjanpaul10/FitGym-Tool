// *********************************************************************************
//	<copyright file="UserQueryRequest.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The User Query Request.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.AIEntities;

/// <summary>
/// The User Query Request.
/// </summary>
public class UserQueryRequest
{
	/// <summary>
	/// Gets or sets the user query.
	/// </summary>
	/// <value>
	/// The user query.
	/// </value>
	public string UserQuery { get; set; } = string.Empty;
}
