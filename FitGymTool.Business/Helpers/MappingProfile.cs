// *********************************************************************************
//	<copyright file="MappingProfile.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Mapping Profile.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.DataAccess.Entity;
using FitGymTool.Shared.DTOs.Members;

namespace FitGymTool.Business.Helpers;

/// <summary>
/// The Mapping Profile.
/// </summary>
/// <seealso cref="Profile"/>
public class MappingProfile : Profile
{
	/// <summary>
	/// Initializes a new instance of <see cref="MappingProfile"/>
	/// </summary>
	public MappingProfile()
	{
		CreateMap<AddMemberDTO, MemberDetails>()
			.ForMember(destination => destination.MemberId, option => option.Ignore())
			.ForMember(destination => destination.MemberGuid, option => option.Ignore())
			.ForMember(destination => destination.IsActive, option => option.Ignore());

		CreateMap<UpdateMemberDTO, MemberDetails>()
			.ForMember(destination => destination.MemberGuid, option => option.Ignore())
			.ForMember(destination => destination.IsActive, option => option.Ignore())
			.ForMember(destination => destination.MemberId, option => option.Ignore());
	}
}
