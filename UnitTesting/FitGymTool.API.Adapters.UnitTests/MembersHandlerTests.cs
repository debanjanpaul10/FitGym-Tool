// *********************************************************************************
//	<copyright file="MembersHandlerTests.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Handler Tests Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Handlers;
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
	}
}
