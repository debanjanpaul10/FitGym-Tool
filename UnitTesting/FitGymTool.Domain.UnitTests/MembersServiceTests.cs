// *********************************************************************************
//	<copyright file="MembersServiceTests.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Service Tests Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.Helpers;
using FitGymTool.Domain.Ports.Out;
using FitGymTool.Domain.UseCases;
using Microsoft.Extensions.Logging;
using Moq;

namespace FitGymTool.Domain.UnitTests;

/// <summary>
/// The Members Service Tests Class.
/// </summary>
public class MembersServiceTests
{
	/// <summary>
	/// The mock members data manager
	/// </summary>
	private readonly Mock<IMembersDataManager> _mockMembersDataManager = new();

	/// <summary>
	/// The mock logger
	/// </summary>
	private readonly Mock<ILogger<MembersService>> _mockLogger = new();

	/// <summary>
	/// The members service
	/// </summary>
	private readonly MembersService _membersService;

	/// <summary>
	/// Initializes a new instance of the <see cref="MembersServiceTests"/> class.
	/// </summary>
	public MembersServiceTests()
	{
		_membersService = new(_mockMembersDataManager.Object, _mockLogger.Object);
	}

	/// <summary>
	/// Adds the new member asynchronous given new member details when request is from admin then add member successfully.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_GivenNewMemberDetails_WhenRequestIsFromAdmin_ThenAddMemberSuccessfully()
	{
		// Arrange
		var mockAddUserDomainData = DomainTestsHelper.PrepareAddMemberDomainData();
		var mockLoggedInUser = DomainTestsHelper.CurrentLoggedInMember;

		_mockMembersDataManager.Setup(x => x.AddNewMemberAsync(It.IsAny<MemberDetails>())).ReturnsAsync(true);

		// Act
		var result = await _membersService.AddNewMemberAsync(mockAddUserDomainData, mockLoggedInUser, true);

		// Assert
		Assert.True(result);
		_mockMembersDataManager.Verify(x => x.AddNewMemberAsync(It.IsAny<MemberDetails>()), Times.Once);
	}

	/// <summary>
	/// Adds the new member asynchronous given new member details when request is from non admin then add member successfully.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_GivenNewMemberDetails_WhenRequestIsFromNonAdmin_ThenAddMemberSuccessfully()
	{
		// Arrange
		var mockAddUserDomainData = DomainTestsHelper.PrepareAddMemberDomainData();
		var mockLoggedInUser = DomainTestsHelper.CurrentLoggedInMember;

		_mockMembersDataManager.Setup(x => x.AddNewMemberAsync(It.IsAny<MemberDetails>())).ReturnsAsync(true);

		// Act
		var result = await _membersService.AddNewMemberAsync(mockAddUserDomainData, mockLoggedInUser, false);

		// Assert
		Assert.True(result);
		_mockMembersDataManager.Verify(x => x.AddNewMemberAsync(It.IsAny<MemberDetails>()), Times.Once);
	}

	/// <summary>
	/// Adds the new member asynchronous given new member details when data manager fails then return false.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_GivenNewMemberDetails_WhenDataManagerFails_ThenReturnFalse()
	{
		// Arrange
		var mockAddUserDomainData = DomainTestsHelper.PrepareAddMemberDomainData();
		var mockLoggedInUser = DomainTestsHelper.CurrentLoggedInMember;

		_mockMembersDataManager.Setup(x => x.AddNewMemberAsync(It.IsAny<MemberDetails>())).ReturnsAsync(false);

		// Act
		var result = await _membersService.AddNewMemberAsync(mockAddUserDomainData, mockLoggedInUser, true);

		// Assert
		Assert.False(result);
		_mockMembersDataManager.Verify(x => x.AddNewMemberAsync(It.IsAny<MemberDetails>()), Times.Once);
	}

	/// <summary>
	/// Adds the new member asynchronous given invalid date values when validation fails then throw invalid operation exception.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_GivenInvalidDateValues_WhenValidationFails_ThenThrowInvalidOperationException()
	{
		// Arrange
		var mockAddUserDomainData = DomainTestsHelper.PrepareAddMemberDomainDataWithInvalidDates();
		var mockLoggedInUser = DomainTestsHelper.CurrentLoggedInMember;

		// Act & Assert
		var exception = await Assert.ThrowsAsync<InvalidOperationException>(
			() => _membersService.AddNewMemberAsync(mockAddUserDomainData, mockLoggedInUser, true));

		Assert.Contains("Invalid date values", exception.Message);
		_mockMembersDataManager.Verify(x => x.AddNewMemberAsync(It.IsAny<MemberDetails>()), Times.Never);
	}

	/// <summary>
	/// Adds the new member asynchronous given new member details when data manager throws exception then propagate exception.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_GivenNewMemberDetails_WhenDataManagerThrowsException_ThenPropagateException()
	{
		// Arrange
		var mockAddUserDomainData = DomainTestsHelper.PrepareAddMemberDomainData();
		var mockLoggedInUser = DomainTestsHelper.CurrentLoggedInMember;
		var expectedException = new Exception("Database connection failed");

		_mockMembersDataManager.Setup(x => x.AddNewMemberAsync(It.IsAny<MemberDetails>())).ThrowsAsync(expectedException);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			() => _membersService.AddNewMemberAsync(mockAddUserDomainData, mockLoggedInUser, true));

		Assert.Equal(expectedException.Message, exception.Message);
		_mockMembersDataManager.Verify(x => x.AddNewMemberAsync(It.IsAny<MemberDetails>()), Times.Once);
	}

	/// <summary>
	/// Gets all members asynchronous when members exist then return list of member details domain.
	/// </summary>
	[Fact]
	public async Task GetAllMembersAsync_WhenMembersExist_ThenReturnListOfMemberDetailsDomain()
	{
		// Arrange
		var mockMembersList = DomainTestsHelper.PrepareMemberDetailsDomainDataList();

		_mockMembersDataManager.Setup(x => x.GetAllMembersAsync()).ReturnsAsync(mockMembersList);

		// Act
		var result = await _membersService.GetAllMembersAsync();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(2, result.Count);
		Assert.Equal(mockMembersList[0].MemberEmail, result[0].MemberEmail);
		Assert.Equal(mockMembersList[1].MemberEmail, result[1].MemberEmail);
		_mockMembersDataManager.Verify(x => x.GetAllMembersAsync(), Times.Once);
	}

	/// <summary>
	/// Gets all members asynchronous when no members exist then return empty list.
	/// </summary>
	[Fact]
	public async Task GetAllMembersAsync_WhenNoMembersExist_ThenReturnEmptyList()
	{
		// Arrange
		var mockMembersList = new List<MemberDetails>();

		_mockMembersDataManager.Setup(x => x.GetAllMembersAsync()).ReturnsAsync(mockMembersList);

		// Act
		var result = await _membersService.GetAllMembersAsync();

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
		_mockMembersDataManager.Verify(x => x.GetAllMembersAsync(), Times.Once);
	}

	/// <summary>
	/// Gets all members asynchronous when data manager throws exception then propagate exception.
	/// </summary>
	[Fact]
	public async Task GetAllMembersAsync_WhenDataManagerThrowsException_ThenPropagateException()
	{
		// Arrange
		var expectedException = new Exception("Database connection failed");

		_mockMembersDataManager.Setup(x => x.GetAllMembersAsync()).ThrowsAsync(expectedException);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(() => _membersService.GetAllMembersAsync());

		Assert.Equal(expectedException.Message, exception.Message);
		_mockMembersDataManager.Verify(x => x.GetAllMembersAsync(), Times.Once);
	}

	/// <summary>
	/// Gets member by email identifier asynchronous given valid email when member exists then return member details domain.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_GivenValidEmail_WhenMemberExists_ThenReturnMemberDetailsDomain()
	{
		// Arrange
		var memberEmail = DomainTestsHelper.CurrentLoggedInMember;
		var mockMemberDetails = DomainTestsHelper.PrepareMemberDetailsDomainData();

		_mockMembersDataManager.Setup(x => x.GetMemberByEmailIdAsync(memberEmail)).ReturnsAsync(mockMemberDetails);

		// Act
		var result = await _membersService.GetMemberByEmailIdAsync(memberEmail);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(mockMemberDetails.MemberEmail, result.MemberEmail);
		Assert.Equal(mockMemberDetails.MemberName, result.MemberName);
		_mockMembersDataManager.Verify(x => x.GetMemberByEmailIdAsync(memberEmail), Times.Once);
	}

	/// <summary>
	/// Gets member by email identifier asynchronous given valid email when member does not exist then throw invalid operation exception.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_GivenValidEmail_WhenMemberDoesNotExist_ThenThrowInvalidOperationException()
	{
		// Arrange
		var memberEmail = "nonexistent@email.com";
		MemberDetails? mockMemberDetails = null;

		_mockMembersDataManager.Setup(x => x.GetMemberByEmailIdAsync(memberEmail)).ReturnsAsync(mockMemberDetails);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<InvalidOperationException>(
			() => _membersService.GetMemberByEmailIdAsync(memberEmail));

		Assert.Contains(DomainConstants.ValidationErrorMessages.MemberNotFoundMessageConstant, exception.Message);
		_mockMembersDataManager.Verify(x => x.GetMemberByEmailIdAsync(memberEmail), Times.Once);
	}

	/// <summary>
	/// Gets member by email identifier asynchronous given valid email when data manager throws exception then propagate exception.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_GivenValidEmail_WhenDataManagerThrowsException_ThenPropagateException()
	{
		// Arrange
		var memberEmail = DomainTestsHelper.CurrentLoggedInMember;
		var expectedException = new Exception("Database connection failed");

		_mockMembersDataManager.Setup(x => x.GetMemberByEmailIdAsync(memberEmail)).ThrowsAsync(expectedException);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			() => _membersService.GetMemberByEmailIdAsync(memberEmail));

		Assert.Equal(expectedException.Message, exception.Message);
		_mockMembersDataManager.Verify(x => x.GetMemberByEmailIdAsync(memberEmail), Times.Once);
	}

	/// <summary>
	/// Updates member details asynchronous given valid member details when update succeeds then return true.
	/// </summary>
	[Fact]
	public async Task UpdateMemberDetailsAsync_GivenValidMemberDetails_WhenUpdateSucceeds_ThenReturnTrue()
	{
		// Arrange
		var mockUpdateMemberDetails = DomainTestsHelper.PrepareUpdateMemberDomainData();

		_mockMembersDataManager.Setup(x => x.UpdateMemberDetailsAsync(It.IsAny<MemberDetails>())).ReturnsAsync(true);

		// Act
		var result = await _membersService.UpdateMemberDetailsAsync(mockUpdateMemberDetails);

		// Assert
		Assert.True(result);
		_mockMembersDataManager.Verify(x => x.UpdateMemberDetailsAsync(It.IsAny<MemberDetails>()), Times.Once);
	}

	/// <summary>
	/// Updates member details asynchronous given valid member details when update fails then return false.
	/// </summary>
	[Fact]
	public async Task UpdateMemberDetailsAsync_GivenValidMemberDetails_WhenUpdateFails_ThenReturnFalse()
	{
		// Arrange
		var mockUpdateMemberDetails = DomainTestsHelper.PrepareUpdateMemberDomainData();

		_mockMembersDataManager.Setup(x => x.UpdateMemberDetailsAsync(It.IsAny<MemberDetails>())).ReturnsAsync(false);

		// Act
		var result = await _membersService.UpdateMemberDetailsAsync(mockUpdateMemberDetails);

		// Assert
		Assert.False(result);
		_mockMembersDataManager.Verify(x => x.UpdateMemberDetailsAsync(It.IsAny<MemberDetails>()), Times.Once);
	}

	/// <summary>
	/// Updates member details asynchronous given valid member details when data manager throws exception then propagate exception.
	/// </summary>
	[Fact]
	public async Task UpdateMemberDetailsAsync_GivenValidMemberDetails_WhenDataManagerThrowsException_ThenPropagateException()
	{
		// Arrange
		var mockUpdateMemberDetails = DomainTestsHelper.PrepareUpdateMemberDomainData();
		var expectedException = new Exception("Database connection failed");

		_mockMembersDataManager.Setup(x => x.UpdateMemberDetailsAsync(It.IsAny<MemberDetails>())).ThrowsAsync(expectedException);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			() => _membersService.UpdateMemberDetailsAsync(mockUpdateMemberDetails));

		Assert.Equal(expectedException.Message, exception.Message);
		_mockMembersDataManager.Verify(x => x.UpdateMemberDetailsAsync(It.IsAny<MemberDetails>()), Times.Once);
	}

	/// <summary>
	/// Updates membership status asynchronous given valid membership status data when update succeeds then return true.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_GivenValidMembershipStatusData_WhenUpdateSucceeds_ThenReturnTrue()
	{
		// Arrange
		var mockUpdateMembershipStatusData = DomainTestsHelper.PrepareUpdateMembershipStatusDomainData();

		_mockMembersDataManager.Setup(x => x.UpdateMembershipStatusAsync(It.IsAny<MemberDetails>())).ReturnsAsync(true);

		// Act
		var result = await _membersService.UpdateMembershipStatusAsync(mockUpdateMembershipStatusData);

		// Assert
		Assert.True(result);
		_mockMembersDataManager.Verify(x => x.UpdateMembershipStatusAsync(It.IsAny<MemberDetails>()), Times.Once);
	}

	/// <summary>
	/// Updates membership status asynchronous given valid membership status data when update fails then return false.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_GivenValidMembershipStatusData_WhenUpdateFails_ThenReturnFalse()
	{
		// Arrange
		var mockUpdateMembershipStatusData = DomainTestsHelper.PrepareUpdateMembershipStatusDomainData();

		_mockMembersDataManager.Setup(x => x.UpdateMembershipStatusAsync(It.IsAny<MemberDetails>())).ReturnsAsync(false);

		// Act
		var result = await _membersService.UpdateMembershipStatusAsync(mockUpdateMembershipStatusData);

		// Assert
		Assert.False(result);
		_mockMembersDataManager.Verify(x => x.UpdateMembershipStatusAsync(It.IsAny<MemberDetails>()), Times.Once);
	}

	/// <summary>
	/// Updates membership status asynchronous given valid membership status data when data manager throws exception then propagate exception.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_GivenValidMembershipStatusData_WhenDataManagerThrowsException_ThenPropagateException()
	{
		// Arrange
		var mockUpdateMembershipStatusData = DomainTestsHelper.PrepareUpdateMembershipStatusDomainData();
		var expectedException = new Exception("Database connection failed");

		_mockMembersDataManager.Setup(x => x.UpdateMembershipStatusAsync(It.IsAny<MemberDetails>())).ThrowsAsync(expectedException);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			() => _membersService.UpdateMembershipStatusAsync(mockUpdateMembershipStatusData));

		Assert.Equal(expectedException.Message, exception.Message);
		_mockMembersDataManager.Verify(x => x.UpdateMembershipStatusAsync(It.IsAny<MemberDetails>()), Times.Once);
	}
}
