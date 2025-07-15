// *********************************************************************************
// <copyright file="DataMapperProfile.cs" company="Personal">
//     Copyright (c) 2025 Personal
// </copyright>
// <summary>The Data Mapper Profile.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Domain.Models.MappingDomain;
using FitGymTool.Infrastructure.DB.Entity.Mapping;

namespace FitGymTool.Infrastructure.DB.Mapper;

/// <summary>
/// The Data Mapper Profile.
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class DataMapperProfile: Profile
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DataMapperProfile"/> class.
	/// </summary>
	public DataMapperProfile()
	{
		CreateMap<FeesPaymentStatusMapping, FeesPaymentStatusMappingDomain>();
		CreateMap<MembershipStatusMapping, MembershipStatusMappingDomain>();
		CreateMap<FeesDurationMapping, FeesDurationMappingDomain>();
	}
}
