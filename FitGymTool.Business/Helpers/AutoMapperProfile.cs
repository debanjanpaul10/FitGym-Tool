// *********************************************************************************
//	<copyright file="AutoMapperProfile.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Auto Mapper Profile.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Infrastructure.DB.Entity;
using FitGymTool.Infrastructure.DB.Entity.Mapping;
using FitGymTool.Shared.DTOs.MappingData;
using FitGymTool.Shared.DTOs.Members;

namespace FitGymTool.Domain.Helpers;

/// <summary>
/// The Auto Mapper Profile.
/// </summary>
/// <seealso cref="Profile"/>
public class AutoMapperProfile : Profile
{
	/// <summary>
	/// Initializes a new instance of <see cref="AutoMapperProfile"/>
	/// </summary>
	public AutoMapperProfile()
	{
		CreateMap<AddMemberDTO, MemberDetails>()
			.ForMember(destination => destination.MemberId, option => option.Ignore())
			.ForMember(destination => destination.MemberGuid, option => option.Ignore())
			.ForMember(destination => destination.IsActive, option => option.Ignore());

		CreateMap<UpdateMemberDTO, MemberDetails>()
			.ForMember(destination => destination.MemberGuid, option => option.Ignore())
			.ForMember(destination => destination.IsActive, option => option.Ignore())
			.ForMember(destination => destination.MemberId, option => option.Ignore());

		CreateMap<MemberDetails, MemberDetailsDTO>()
			.ForMember(dest => dest.MembershipStatus, 
				opt => opt.MapFrom(src => src.MembershipStatusMapping != null ? src.MembershipStatusMapping.StatusName : string.Empty));

		CreateMap<FeesDurationMapping, FeesDurationMappingDto>().ReverseMap()
			.ForMember(dest => dest.IsActive, option => option.Ignore())
			.ForMember(dest => dest.DateCreated, option => option.Ignore())
			.ForMember(dest => dest.CreatedBy, option => option.Ignore())
			.ForMember(dest => dest.DateModified, option => option.Ignore())
			.ForMember(dest => dest.ModifiedBy, option => option.Ignore());

		CreateMap<FeesPaymentStatusMapping, FeesPaymentStatusMappingDto>().ReverseMap()
			.ForMember(dest => dest.IsActive, option => option.Ignore())
			.ForMember(dest => dest.DateCreated, option => option.Ignore())
			.ForMember(dest => dest.CreatedBy, option => option.Ignore())
			.ForMember(dest => dest.DateModified, option => option.Ignore())
			.ForMember(dest => dest.ModifiedBy, option => option.Ignore());

		CreateMap<MembershipStatusMapping, MembershipStatusMappingDto>().ReverseMap()
			.ForMember(dest => dest.IsActive, option => option.Ignore())
			.ForMember(dest => dest.DateCreated, option => option.Ignore())
			.ForMember(dest => dest.CreatedBy, option => option.Ignore())
			.ForMember(dest => dest.DateModified, option => option.Ignore())
			.ForMember(dest => dest.ModifiedBy, option => option.Ignore());
	}
}
