// *********************************************************************************
// <copyright file="DataMapperProfile.cs" company="Personal">
//     Copyright (c) 2025 Personal
// </copyright>
// <summary>The Data Mapper Profile.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Domain.Models.MappingDomain;
using FitGymTool.Domain.Models.Members;
using FitGymTool.Infrastructure.DB.Entity;
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
		
		// Members mapping configurations
		CreateMap<MemberDetails, MemberDetailsDomain>()
			.ForMember(destination => destination.MembershipStatus,
				option => option.MapFrom(source => source.MembershipStatusMapping != null ? source.MembershipStatusMapping.StatusName : string.Empty))
			.ReverseMap();

		CreateMap<AddMemberDomain, MemberDetails>()
			.ForMember(destination => destination.MembershipStatusId,
				option => option.MapFrom(source => 0));
		
		CreateMap<MemberDetails, UpdateMemberDomain>()
			.ForMember(destination => destination.MembershipStatusId,
				option => option.MapFrom(source => source.MembershipStatusMapping != null ? source.MembershipStatusMapping.Id : 0))
			.ReverseMap();
	}
}
