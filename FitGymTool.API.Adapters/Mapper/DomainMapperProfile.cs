// *********************************************************************************
//	<copyright file="DomainMapperProfile.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Domain Mapper Profile Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.MappingData;
using FitGymTool.API.Adapters.Models.Response.Members;
using FitGymTool.Domain.Models;
using FitGymTool.Domain.Models.MappingDomain;
using FitGymTool.Domain.Models.Members;

namespace FitGymTool.API.Adapters.Mapper;

/// <summary>
/// The Domain Mapper Profile Class.
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class DomainMapperProfile: Profile
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DomainMapperProfile"/> class.
	/// </summary>
	public DomainMapperProfile()
	{
		CreateMap<MappingMasterDataDomain, MappingMasterDataDto>();
		CreateMap<FeesPaymentStatusMappingDomain, FeesPaymentStatusMappingDto>();
		CreateMap<MembershipStatusMappingDomain, MembershipStatusMappingDto>();
		CreateMap<FeesDurationMappingDomain, FeesDurationMappingDto>();
		CreateMap<CurrentMonthFeesAndRevenueStatusDomain, CurrentMonthFeesAndRevenueStatusDto>();
		
		// Members mapping configurations
		CreateMap<AddMemberDTO, MemberDetailsDomain>();
		CreateMap<UpdateMemberDTO, MemberDetailsDomain>();
		CreateMap<MemberDetailsDomain, MemberDetailsDTO>()
			.ForMember(destination => destination.MembershipStatus, option => option.MapFrom(source => source.MembershipStatus));
	}
}
