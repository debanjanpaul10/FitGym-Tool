// *********************************************************************************
//	<copyright file="DomainMapperProfile.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Domain Mapper Profile Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.DerivedEntities;
using FitGymTool.API.Adapters.Models.Response.MappingData;
using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using FitGymTool.Domain.DomainEntities.Mapping;
using System.Diagnostics.CodeAnalysis;

namespace FitGymTool.API.Adapters.Mapper;

/// <summary>
/// The Domain Mapper Profile Class.
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
[ExcludeFromCodeCoverage]
public class DomainMapperProfile : Profile
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DomainMapperProfile"/> class.
	/// </summary>
	public DomainMapperProfile()
	{
		// ENTITIES
		CreateMap<CurrentMonthFeesAndRevenueStatus, CurrentMonthFeesAndRevenueStatusDTO>();
		CreateMap<UpdateMemberDTO, MemberDetails>();
		CreateMap<MemberDetails, MemberDetailsDTO>()
			.ForMember(destination => destination.MembershipStatus, option => option.MapFrom(source => source.MembershipStatusMapping != null ? source.MembershipStatusMapping.StatusName : string.Empty));
		CreateMap<AddBugReportDTO, BugReportData>()
			.ForMember(destination => destination.Id, options => options.Ignore())
			.ForMember(destination => destination.BugStatusId, options => options.Ignore())
			.ForMember(dest => dest.BugSeverityId, option => option.MapFrom(src => src.BugSeverity))
			.ForMember(destination => destination.Title, option => option.MapFrom(source => source.BugTitle))
			.ForMember(destination => destination.Description, option => option.MapFrom(source => source.BugDescription));
		CreateMap<UpdateMembershipStatusDTO, MemberDetails>()
			.ForMember(dest => dest.MemberEmail, option => option.MapFrom(source => source.MemberEmailAddress));
		CreateMap<FeesStructure, FeesStructureDTO>()
			.ForMember(dest => dest.FeesDuration, option => option.MapFrom(source => source.FeesDurationMapping != null ? source.FeesDurationMapping.DurationTypeName : string.Empty));
		CreateMap<CurrentMembersFeesStatus, CurrentMembersFeesStatusDTO>();
		CreateMap<MemberPaymentHistoryData, MemberPaymentHistoryDTO>();
		CreateMap<UpdateMemberFeesDTO, UpdateMemberFees>().ForMember(destination => destination.ModifiedBy, options => options.Ignore());
		CreateMap<BugSeverityInput, BugSeverityInputDTO>().ReverseMap();
		CreateMap<BugSeverityResponse, BugSeverityResponseDTO>().ReverseMap();
		CreateMap<AddMemberDTO, NewMemberDetails>()
			.ForMember(destination => destination.MembershipStatusId, option => option.Ignore()).ReverseMap()
			.ForMember(destination => destination.MembershipStatus, option => option.MapFrom(source => source.MembershipStatusMapping != null ? source.MembershipStatusMapping.StatusName : string.Empty));
		CreateMap<AIFeature, AIFeaturesDTO>();

		// MAPPING
		CreateMap<MappingMasterData, MappingMasterDataDto>();
		CreateMap<FeesPaymentStatusMapping, FeesPaymentStatusMappingDto>();
		CreateMap<MembershipStatusMapping, MembershipStatusMappingDto>();
		CreateMap<FeesDurationMapping, FeesDurationMappingDto>();
		CreateMap<BugSeverityMapping, BugSeverityMappingDto>()
			.ForMember(destination => destination.Id, option => option.MapFrom(source => source.Id))
			.ForMember(destination => destination.SeverityName, option => option.MapFrom(source => source.SeverityName));
	}
}
