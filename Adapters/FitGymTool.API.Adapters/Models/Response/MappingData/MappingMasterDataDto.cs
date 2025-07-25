﻿// *********************************************************************************
//	<copyright file="MappingMasterDataDto.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Mapping Master Data DTO.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response.MappingData;

/// <summary>
/// The Mapping Master Data DTO.
/// </summary>
public class MappingMasterDataDto
{
	/// <summary>
	/// Gets or sets the fees duration mapping.
	/// </summary>
	/// <value>
	/// The fees duration mapping.
	/// </value>
	public IEnumerable<FeesDurationMappingDto> FeesDurationMapping { get; set; } = [];

	/// <summary>
	/// Gets or sets the fees payment status mapping.
	/// </summary>
	/// <value>
	/// The fees payment status mapping.
	/// </value>
	public IEnumerable<FeesPaymentStatusMappingDto> FeesPaymentStatusMapping { get; set; } = [];

	/// <summary>
	/// Gets or sets the membership status mapping.
	/// </summary>
	/// <value>
	/// The membership status mapping.
	/// </value>
	public IEnumerable<MembershipStatusMappingDto> MembershipStatusMapping { get; set; } = [];

	/// <summary>
	/// Gets or sets the bug severity mapping.
	/// </summary>
	/// <value>
	/// The bug severity mapping.
	/// </value>
	public IEnumerable<BugSeverityMappingDto> BugSeverityMapping { get; set; } = [];
}
