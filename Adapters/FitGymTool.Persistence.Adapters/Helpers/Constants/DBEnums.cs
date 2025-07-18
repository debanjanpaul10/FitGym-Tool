// *********************************************************************************
//	<copyright file="DBEnums.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Class.</summary>
// *********************************************************************************

using System.ComponentModel;

namespace FitGymTool.Persistence.Adapters.Helpers.Constants;

/// <summary>
/// The Database Enums Class.
/// </summary>
public static class DBEnums
{
	public enum MemberShipStatus
	{
		[Description("Active")]
		Active,

		[Description("On Termination")]
		OnTermination,

		[Description("Expired")]
		Expired
	}

}
