// *********************************************************************************
//	<copyright file="DomainMapperProfile.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Domain Mapper Profile Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.MappingData;
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
		CreateMap<BugSeverityMappingDomain, BugSeverityMappingDto>()
			.ForMember(destination => destination.Id, option => option.MapFrom(source => source.Id))
			.ForMember(destination => destination.SeverityName, option => option.MapFrom(source => source.SeverityName));

		// Members mapping configurations
		CreateMap<AddMemberDTO, AddMemberDomain>()
			.ForMember(destination => destination.MembershipStatus, option => option.MapFrom(source => source.MembershipStatus));
		CreateMap<UpdateMemberDTO, UpdateMemberDomain>();
		CreateMap<MemberDetailsDomain, MemberDetailsDTO>()
			.ForMember(destination => destination.MembershipStatus, option => option.MapFrom(source => source.MembershipStatus));
		
		CreateMap<AddBugReportDTO, BugReportDataDomain>()
			.ForMember(destination => destination.Id, options => options.Ignore())
			.ForMember(destination => destination.BugSeverityId, options => options.Ignore())
			.ForMember(destination => destination.BugStatusId, options => options.Ignore())
			.ForMember(destination => destination.Title, option => option.MapFrom(source => source.BugTitle))
			.ForMember(destination => destination.Description, option => option.MapFrom(source => source.BugDescription));
		CreateMap<UpdateMembershipStatusDTO, UpdateMembershipStatusDomain>();
	}
}
