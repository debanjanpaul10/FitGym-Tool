// *********************************************************************************
// <copyright file="DataMapperProfile.cs" company="Personal">
//     Copyright (c) 2025 <Debanjan's Lab>
// </copyright>
// <summary>The Data Mapper Profile.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Domain.Models;
using FitGymTool.Domain.Models.MappingDomain;
using FitGymTool.Domain.Models.Members;
using FitGymTool.Persistence.Adapters.Entity;
using FitGymTool.Persistence.Adapters.Entity.Mapping;

namespace FitGymTool.Persistence.Adapters.Mapper;

/// <summary>
/// The Data Mapper Profile.
/// </summary>
/// <seealso cref="Profile" />
public class DataMapperProfile: Profile
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DataMapperProfile"/> class.
	/// </summary>
	public DataMapperProfile()
	{
		// Lookup mapping data
		CreateMap<FeesPaymentStatusMapping, FeesPaymentStatusMappingDomain>().ReverseMap();
		CreateMap<MembershipStatusMapping, MembershipStatusMappingDomain>().ReverseMap();
		CreateMap<FeesDurationMapping, FeesDurationMappingDomain>().ReverseMap();
		CreateMap<BugItemStatusMapping, BugItemStatusMappingDomain>().ReverseMap();
		CreateMap<BugSeverityMapping, BugSeverityMappingDomain>().ReverseMap();
		
		// Members mapping configurations
		CreateMap<MemberDetails, MemberDetailsDomain>()
			.ForMember(destination => destination.MembershipStatus,
				option => option.MapFrom(source => source.MembershipStatusMapping != null ? source.MembershipStatusMapping.StatusName : string.Empty))
			.ReverseMap();

		CreateMap<AddMemberDomain, MemberDetails>()
			.ForMember(destination => destination.MembershipStatusId,
				option => option.MapFrom(source => 0));

		CreateMap<UpdateMemberDomain, MemberDetails>()
			.ForMember(destination => destination.MemberId, options => options.Ignore())
			.ForMember(destination => destination.IsActive, options => options.Ignore())
			.ForMember(destination => destination.CreatedBy, options => options.Ignore())
			.ForMember(destination => destination.DateCreated, options => options.Ignore());

		CreateMap<BugReportDataDomain, BugReportData>();
		CreateMap<UpdateMembershipStatusDomain, MemberDetails>()
			.ReverseMap()
			.ForMember(destination => destination.MemberEmailAddress, option => option.Ignore())
			.ForMember(destination => destination.ModifiedBy, option => option.Ignore());
	}
}
