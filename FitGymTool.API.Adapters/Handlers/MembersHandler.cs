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
using FitGymTool.Domain.Models.Members;

namespace FitGymTool.API.Adapters.Handlers;

/// <summary>
/// The Members Handler Adapter Class.
/// </summary>
/// <param name="mapper">The mapper.</param>
/// <param name="membersService">The Members service.</param>
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
	public async Task<bool> AddNewMemberAsync(AddMemberDTO memberDetails, string userEmail, bool isFromAdmin)
	{
		var domainRequest = this._mapper.Map<AddMemberDomain>(memberDetails);
		return await this._membersService.AddNewMemberAsync(domainRequest, userEmail, isFromAdmin);
	}

	/// <summary>
	/// Deletes a member by MemberId asynchronously.
	/// </summary>
	/// <param name="memberId">The member's ID.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> DeleteMemberAsync(int memberId)
	{
		return await this._membersService.DeleteMemberAsync(memberId);
	}

	/// <summary>
	/// Gets all members from the database asynchronously.
	/// </summary>
	/// <returns>A list of MemberDetailsDTO.</returns>
	public async Task<List<MemberDetailsDTO>> GetAllMembersAsync()
	{
		var domainMembers = await this._membersService.GetAllMembersAsync();
		return this._mapper.Map<List<MemberDetailsDTO>>(domainMembers);
	}

	/// <summary>
	/// Gets a single member's details by Member's Email ID asynchronously.
	/// </summary>
	/// <param name="memberEmail">The member's Email ID.</param>
	/// <returns>The MemberDetailsDTO object if found; otherwise, null.</returns>
	public async Task<MemberDetailsDTO> GetMemberByEmailIdAsync(string memberEmail)
	{
		var domainMember = await this._membersService.GetMemberByEmailIdAsync(memberEmail);
		return this._mapper.Map<MemberDetailsDTO>(domainMember);
	}

	/// <summary>
	/// Updates an existing member's details asynchronously.
	/// </summary>
	/// <param name="memberDetails">The updated member details.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMemberAsync(UpdateMemberDTO memberDetails)
	{
		var domainRequest = this._mapper.Map<UpdateMemberDomain>(memberDetails);
		return await this._membersService.UpdateMemberAsync(domainRequest);
	}
}
