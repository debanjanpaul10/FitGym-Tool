// *********************************************************************************
//	<copyright file="MembersHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Handler Adapter Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.Domain.Models.Members;
using FitGymTool.Domain.Ports.In;

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
		var domainRequest = _mapper.Map<AddMemberDomain>(memberDetails);
		return await _membersService.AddNewMemberAsync(domainRequest, userEmail, isFromAdmin);
	}

	/// <summary>
	/// Gets all members from the database asynchronously.
	/// </summary>
	/// <returns>A list of MemberDetailsDTO.</returns>
	public async Task<List<MemberDetailsDTO>> GetAllMembersAsync()
	{
		var domainMembers = await _membersService.GetAllMembersAsync();
		return _mapper.Map<List<MemberDetailsDTO>>(domainMembers);
	}

	/// <summary>
	/// Gets a single member's details by Member's Email ID asynchronously.
	/// </summary>
	/// <param name="memberEmail">The member's Email ID.</param>
	/// <returns>The MemberDetailsDTO object if found; otherwise, null.</returns>
	public async Task<MemberDetailsDTO> GetMemberByEmailIdAsync(string memberEmail)
	{
		var domainMember = await _membersService.GetMemberByEmailIdAsync(memberEmail);
		return _mapper.Map<MemberDetailsDTO>(domainMember);
	}

	/// <summary>
	/// Updates an existing member's details asynchronously.
	/// </summary>
	/// <param name="memberDetails">The updated member details.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMemberDetailsAsync(UpdateMemberDTO memberDetails)
	{
		var domainRequest = _mapper.Map<UpdateMemberDomain>(memberDetails);
		return await _membersService.UpdateMemberDetailsAsync(domainRequest);
	}

	/// <summary>
	/// Updates the membership status asynchronous.
	/// </summary>
	/// <param name="updateMembershipStatusDto">The update membership status dto.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMembershipStatusAsync(UpdateMembershipStatusDTO updateMembershipStatusDto)
	{
		var domainRequest = _mapper.Map<UpdateMembershipStatusDomain>(updateMembershipStatusDto);
		return await _membersService.UpdateMembershipStatusAsync(domainRequest);
	}
}
