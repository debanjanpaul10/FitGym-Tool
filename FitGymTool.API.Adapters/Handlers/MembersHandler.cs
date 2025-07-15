// *********************************************************************************
//	<copyright file="MembersHandler.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Handler Adapter Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Response.Members;
using FitGymTool.Domain.DrivingPorts;

namespace FitGymTool.API.Adapters.Handlers;

/// <summary>
/// The Members Handler Adapter Class.
/// </summary>
/// <seealso cref="FitGymTool.API.Adapters.Contracts.IMembersHandler" />
public class MembersHandler(IMembersService membersService, IMapper mapper) : IMembersHandler
{
	/// <summary>
	/// The members service
	/// </summary>
	private readonly IMembersService _membersService = membersService;

	/// <summary>
	/// The mapper
	/// </summary>
	private readonly IMapper _mapper = mapper;

	/// <summary>
	/// Adds a new member to the database asynchronously.
	/// </summary>
	/// <param name="memberDetails">The member details data.</param>
	/// <param name="userEmail">The user email.</param>
	/// <param name="isFromAdmin">The boolean flag to indicate admin request.</param>
	/// <returns>
	/// The boolean result for success/failure.
	/// </returns>
	/// <exception cref="System.NotImplementedException"></exception>
	public Task<bool> AddNewMemberAsync(AddMemberDTO memberDetails, string userEmail, bool isFromAdmin)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteMemberAsync(int memberId)
	{
		throw new NotImplementedException();
	}

	public Task<List<MemberDetailsDTO>> GetAllMembersAsync()
	{
		throw new NotImplementedException();
	}

	public Task<MemberDetailsDTO> GetMemberByEmailIdAsync(string memberEmail)
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdateMemberAsync(UpdateMemberDTO memberDetails)
	{
		throw new NotImplementedException();
	}
}
