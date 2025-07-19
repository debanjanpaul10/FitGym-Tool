// *********************************************************************************
//	<copyright file="MembersHandlerTests.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Handler Tests Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Handlers;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.Domain.Models.Members;
using FitGymTool.Domain.Ports.In;
using Moq;

namespace FitGymTool.API.Adapters.UnitTests;

/// <summary>
/// The Members Handler Tests Class.
/// </summary>
public class MembersHandlerTests
{
	/// <summary>
	/// The mock members service
	/// </summary>
	private readonly Mock<IMembersService> _mockMembersService = new();

	/// <summary>
	/// The mock mapper
	/// </summary>
	private readonly Mock<IMapper> _mockMapper = new();

	/// <summary>
	/// The members handler
	/// </summary>
	private readonly MembersHandler _membersHandler;

	/// <summary>
	/// Initializes a new instance of the <see cref="MembersHandlerTests"/> class.
	/// </summary>
	public MembersHandlerTests()
	{
		_membersHandler = new(_mockMembersService.Object, _mockMapper.Object);
	}

	/// <summary>
	/// Adds the new member asynchronous given member details data when member added by admin then successfully save member data.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_GivenMemberDetailsData_WhenMemberAddedByAdmin_ThenSuccessfullySaveMemberData()
	{
		// Arrange
		var mockAddUserDataDto = ApiAdaptersTestsHelper.PrepareAddMemberDataDto();
		var mockUserEmail = ApiAdaptersTestsHelper.CurrentLoggedInUser;

		_mockMembersService.Setup(x => x.AddNewMemberAsync(It.IsAny<AddMemberDomain>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(true);

		// Act
		var result = await _membersHandler.AddNewMemberAsync(mockAddUserDataDto, mockUserEmail, true);

		// Assert
		Assert.True(result);
		_mockMembersService.Verify(x => x.AddNewMemberAsync(It.IsAny<AddMemberDomain>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
	}

	/// <summary>
	/// Adds the new member asynchronous given member details data when member added by non admin then successfully save member data.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_GivenMemberDetailsData_WhenMemberAddedByNonAdmin_ThenSuccessfullySaveMemberData()
	{
		// Arrange
		var mockAddUserDataDto = ApiAdaptersTestsHelper.PrepareAddMemberDataDto();
		var mockUserEmail = ApiAdaptersTestsHelper.CurrentLoggedInUser;

		_mockMembersService.Setup(x => x.AddNewMemberAsync(It.IsAny<AddMemberDomain>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(true);

		// Act
		var result = await _membersHandler.AddNewMemberAsync(mockAddUserDataDto, mockUserEmail, false);

		// Assert
		Assert.True(result);
		_mockMembersService.Verify(x => x.AddNewMemberAsync(It.IsAny<AddMemberDomain>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
	}

	/// <summary>
	/// Adds the new member asynchronous given member details data when service fails then return false.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_GivenMemberDetailsData_WhenServiceFails_ThenReturnFalse()
	{
		// Arrange
		var mockAddUserDataDto = ApiAdaptersTestsHelper.PrepareAddMemberDataDto();
		var mockUserEmail = ApiAdaptersTestsHelper.CurrentLoggedInUser;

		_mockMembersService.Setup(x => x.AddNewMemberAsync(It.IsAny<AddMemberDomain>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(false);

		// Act
		var result = await _membersHandler.AddNewMemberAsync(mockAddUserDataDto, mockUserEmail, true);

		// Assert
		Assert.False(result);
	}

	/// <summary>
	/// Gets all members asynchronous when members exist then return list of member details dto.
	/// </summary>
	[Fact]
	public async Task GetAllMembersAsync_WhenMembersExist_ThenReturnListOfMemberDetailsDto()
	{
		// Arrange
		var mockDomainMembers = ApiAdaptersTestsHelper.PrepareMemberDetailsDomainList();
		var mockDtoMembers = new List<MemberDetailsDTO>
		{
			ApiAdaptersTestsHelper.PrepareMemberDetailsDto(),
			ApiAdaptersTestsHelper.PrepareMemberDetailsDto()
		};

		_mockMembersService.Setup(x => x.GetAllMembersAsync()).ReturnsAsync(mockDomainMembers);
		_mockMapper.Setup(x => x.Map<List<MemberDetailsDTO>>(It.IsAny<List<MemberDetailsDomain>>())).Returns(mockDtoMembers);

		// Act
		var result = await _membersHandler.GetAllMembersAsync();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(2, result.Count);
		_mockMembersService.Verify(x => x.GetAllMembersAsync(), Times.Once);
		_mockMapper.Verify(x => x.Map<List<MemberDetailsDTO>>(It.IsAny<List<MemberDetailsDomain>>()), Times.Once);
	}

	/// <summary>
	/// Gets all members asynchronous when no members exist then return empty list.
	/// </summary>
	[Fact]
	public async Task GetAllMembersAsync_WhenNoMembersExist_ThenReturnEmptyList()
	{
		// Arrange
		var mockDomainMembers = new List<MemberDetailsDomain>();
		var mockDtoMembers = new List<MemberDetailsDTO>();

		_mockMembersService.Setup(x => x.GetAllMembersAsync()).ReturnsAsync(mockDomainMembers);
		_mockMapper.Setup(x => x.Map<List<MemberDetailsDTO>>(It.IsAny<List<MemberDetailsDomain>>())).Returns(mockDtoMembers);

		// Act
		var result = await _membersHandler.GetAllMembersAsync();

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
		_mockMembersService.Verify(x => x.GetAllMembersAsync(), Times.Once);
		_mockMapper.Verify(x => x.Map<List<MemberDetailsDTO>>(It.IsAny<List<MemberDetailsDomain>>()), Times.Once);
	}

	/// <summary>
	/// Gets member by email identifier asynchronous given valid email when member exists then return member details dto.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_GivenValidEmail_WhenMemberExists_ThenReturnMemberDetailsDto()
	{
		// Arrange
		var memberEmail = ApiAdaptersTestsHelper.CurrentLoggedInUser;
		var mockDomainMember = ApiAdaptersTestsHelper.PrepareMemberDetailsDomain();
		var mockDtoMember = ApiAdaptersTestsHelper.PrepareMemberDetailsDto();

		_mockMembersService.Setup(x => x.GetMemberByEmailIdAsync(memberEmail)).ReturnsAsync(mockDomainMember);
		_mockMapper.Setup(x => x.Map<MemberDetailsDTO>(It.IsAny<MemberDetailsDomain>())).Returns(mockDtoMember);

		// Act
		var result = await _membersHandler.GetMemberByEmailIdAsync(memberEmail);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(mockDtoMember.MemberEmail, result.MemberEmail);
		_mockMembersService.Verify(x => x.GetMemberByEmailIdAsync(memberEmail), Times.Once);
		_mockMapper.Verify(x => x.Map<MemberDetailsDTO>(It.IsAny<MemberDetailsDomain>()), Times.Once);
	}

	/// <summary>
	/// Gets member by email identifier asynchronous given valid email when member does not exist then return null.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_GivenValidEmail_WhenMemberDoesNotExist_ThenReturnNull()
	{
		// Arrange
		var memberEmail = "nonexistent@email.com";
		MemberDetailsDomain? mockDomainMember = null!;
		MemberDetailsDTO? mockDtoMember = null!;

		_mockMembersService.Setup(x => x.GetMemberByEmailIdAsync(It.IsAny<string>())).ReturnsAsync(mockDomainMember);
		_mockMapper.Setup(x => x.Map<MemberDetailsDTO>(It.IsAny<MemberDetailsDomain>())).Returns(mockDtoMember);

		// Act
		var result = await _membersHandler.GetMemberByEmailIdAsync(memberEmail);

		// Assert
		Assert.Null(result);
		_mockMembersService.Verify(x => x.GetMemberByEmailIdAsync(memberEmail), Times.Once);
		_mockMapper.Verify(x => x.Map<MemberDetailsDTO>(It.IsAny<MemberDetailsDomain>()), Times.Once);
	}

	/// <summary>
	/// Updates member details asynchronous given valid member details when update succeeds then return true.
	/// </summary>
	[Fact]
	public async Task UpdateMemberDetailsAsync_GivenValidMemberDetails_WhenUpdateSucceeds_ThenReturnTrue()
	{
		// Arrange
		var mockUpdateMemberDto = ApiAdaptersTestsHelper.PrepareUpdateMemberDataDto();

		_mockMembersService.Setup(x => x.UpdateMemberDetailsAsync(It.IsAny<UpdateMemberDomain>())).ReturnsAsync(true);

		// Act
		var result = await _membersHandler.UpdateMemberDetailsAsync(mockUpdateMemberDto);

		// Assert
		Assert.True(result);
		_mockMembersService.Verify(x => x.UpdateMemberDetailsAsync(It.IsAny<UpdateMemberDomain>()), Times.Once);
		_mockMapper.Verify(x => x.Map<UpdateMemberDomain>(It.IsAny<UpdateMemberDTO>()), Times.Once);
	}

	/// <summary>
	/// Updates member details asynchronous given valid member details when update fails then return false.
	/// </summary>
	[Fact]
	public async Task UpdateMemberDetailsAsync_GivenValidMemberDetails_WhenUpdateFails_ThenReturnFalse()
	{
		// Arrange
		var mockUpdateMemberDto = ApiAdaptersTestsHelper.PrepareUpdateMemberDataDto();

		_mockMembersService.Setup(x => x.UpdateMemberDetailsAsync(It.IsAny<UpdateMemberDomain>())).ReturnsAsync(false);

		// Act
		var result = await _membersHandler.UpdateMemberDetailsAsync(mockUpdateMemberDto);

		// Assert
		Assert.False(result);
		_mockMembersService.Verify(x => x.UpdateMemberDetailsAsync(It.IsAny<UpdateMemberDomain>()), Times.Once);
		_mockMapper.Verify(x => x.Map<UpdateMemberDomain>(It.IsAny<UpdateMemberDTO>()), Times.Once);
	}

	/// <summary>
	/// Updates membership status asynchronous given valid membership status data when update succeeds then return true.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_GivenValidMembershipStatusData_WhenUpdateSucceeds_ThenReturnTrue()
	{
		// Arrange
		var mockUpdateMembershipStatusDto = ApiAdaptersTestsHelper.PrepareUpdateMembershipStatusDto();

		_mockMembersService.Setup(x => x.UpdateMembershipStatusAsync(It.IsAny<UpdateMembershipStatusDomain>())).ReturnsAsync(true);

		// Act
		var result = await _membersHandler.UpdateMembershipStatusAsync(mockUpdateMembershipStatusDto);

		// Assert
		Assert.True(result);
		_mockMembersService.Verify(x => x.UpdateMembershipStatusAsync(It.IsAny<UpdateMembershipStatusDomain>()), Times.Once);
		_mockMapper.Verify(x => x.Map<UpdateMembershipStatusDomain>(It.IsAny<UpdateMembershipStatusDTO>()), Times.Once);
	}

	/// <summary>
	/// Updates membership status asynchronous given valid membership status data when update fails then return false.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_GivenValidMembershipStatusData_WhenUpdateFails_ThenReturnFalse()
	{
		// Arrange
		var mockUpdateMembershipStatusDto = ApiAdaptersTestsHelper.PrepareUpdateMembershipStatusDto();

		_mockMembersService.Setup(x => x.UpdateMembershipStatusAsync(It.IsAny<UpdateMembershipStatusDomain>())).ReturnsAsync(false);

		// Act
		var result = await _membersHandler.UpdateMembershipStatusAsync(mockUpdateMembershipStatusDto);

		// Assert
		Assert.False(result);
		_mockMembersService.Verify(x => x.UpdateMembershipStatusAsync(It.IsAny<UpdateMembershipStatusDomain>()), Times.Once);
		_mockMapper.Verify(x => x.Map<UpdateMembershipStatusDomain>(It.IsAny<UpdateMembershipStatusDTO>()), Times.Once);
	}
}
