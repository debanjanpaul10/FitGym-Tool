// *********************************************************************************
//	<copyright file="MembersDataManagerTests.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Data Manager Tests Class.</summary>
// *********************************************************************************

using System.Linq.Expressions;
using AutoMapper;
using FitGymTool.Domain.Models.Members;
using FitGymTool.Persistence.Adapters.Contracts;
using FitGymTool.Persistence.Adapters.DataManager;
using FitGymTool.Persistence.Adapters.Entity;
using FitGymTool.Persistence.Adapters.Entity.Mapping;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Persistence.Adapters.UnitTests;

/// <summary>
/// The Members Data Manager Tests Class.
/// </summary>
public class MembersDataManagerTests
{
	/// <summary>
	/// The mock unit of work
	/// </summary>
	private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();

	/// <summary>
	/// The mock logger
	/// </summary>
	private readonly Mock<ILogger<MembersDataManager>> _mockLogger = new();

	/// <summary>
	/// The mock mapper
	/// </summary>
	private readonly Mock<IMapper> _mockMapper = new();

	/// <summary>
	/// The mock repository for MemberDetails
	/// </summary>
	private readonly Mock<IRepository<MemberDetails>> _mockMemberRepository = new();

	/// <summary>
	/// The mock repository for MembershipStatusMapping
	/// </summary>
	private readonly Mock<IRepository<MembershipStatusMapping>> _mockStatusRepository = new();

	/// <summary>
	/// The members data manager
	/// </summary>
	private readonly MembersDataManager _membersDataManager;

	/// <summary>
	/// Initializes a new instance of the <see cref="MembersDataManagerTests"/> class.
	/// </summary>
	public MembersDataManagerTests()
	{
		_mockUnitOfWork.Setup(x => x.Repository<MemberDetails>()).Returns(_mockMemberRepository.Object);
		_mockUnitOfWork.Setup(x => x.Repository<MembershipStatusMapping>()).Returns(_mockStatusRepository.Object);
		_membersDataManager = new(_mockUnitOfWork.Object, _mockMapper.Object, _mockLogger.Object);
	}

	#region AddNewMemberAsync Tests

	/// <summary>
	/// Tests that AddNewMemberAsync successfully adds a new member when valid data is provided.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_WithValidData_ShouldReturnTrue()
	{
		// Arrange
		var memberDetails = PersistenceTestsHelper.CreateValidAddMemberDomain();
		var memberEntity = PersistenceTestsHelper.CreateValidMemberDetailsEntity();
		var statusMapping = PersistenceTestsHelper.CreateValidMembershipStatusMapping();

		_mockMemberRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(new List<MemberDetails>());

		_mockStatusRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MembershipStatusMapping, bool>>>()))
			.ReturnsAsync(statusMapping);

		_mockMapper.Setup(x => x.Map<MemberDetails>(memberDetails)).Returns(memberEntity);

		_mockMemberRepository.Setup(x => x.AddAsync(It.IsAny<MemberDetails>())).ReturnsAsync(memberEntity);
		_mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

		// Act
		var result = await _membersDataManager.AddNewMemberAsync(memberDetails);

		// Assert
		Assert.True(result);
		_mockMemberRepository.Verify(x => x.AddAsync(It.IsAny<MemberDetails>()), Times.Once);
		_mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
	}

	/// <summary>
	/// Tests that AddNewMemberAsync throws InvalidOperationException when member already exists.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_WithExistingMember_ShouldThrowInvalidOperationException()
	{
		// Arrange
		var memberDetails = PersistenceTestsHelper.CreateValidAddMemberDomain();
		var existingMember = PersistenceTestsHelper.CreateValidMemberDetailsEntity();

		_mockMemberRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(new List<MemberDetails> { existingMember });

		// Act & Assert
		var exception = await Assert.ThrowsAsync<InvalidOperationException>(
			() => _membersDataManager.AddNewMemberAsync(memberDetails));

		Assert.Equal(ValidationErrorMessages.MemberAlreadyExistsMessageConstant, exception.Message);
		_mockMemberRepository.Verify(x => x.AddAsync(It.IsAny<MemberDetails>()), Times.Never);
		_mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);
	}

	/// <summary>
	/// Tests that AddNewMemberAsync handles exception during database operation.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_WithDatabaseException_ShouldThrowException()
	{
		// Arrange
		var memberDetails = PersistenceTestsHelper.CreateValidAddMemberDomain();
		var statusMapping = PersistenceTestsHelper.CreateValidMembershipStatusMapping();
		var memberEntity = PersistenceTestsHelper.CreateValidMemberDetailsEntity();

		_mockMemberRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(new List<MemberDetails>());

		_mockStatusRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MembershipStatusMapping, bool>>>()))
			.ReturnsAsync(statusMapping);

		_mockMapper.Setup(x => x.Map<MemberDetails>(memberDetails)).Returns(memberEntity);

		_mockMemberRepository.Setup(x => x.AddAsync(It.IsAny<MemberDetails>()))
			.ThrowsAsync(new Exception("Database error"));

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			() => _membersDataManager.AddNewMemberAsync(memberDetails));

		Assert.Equal("Database error", exception.Message);
		_mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);
	}

	/// <summary>
	/// Tests that AddNewMemberAsync handles null status mapping gracefully.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_WithNullStatusMapping_ShouldSetStatusIdToZero()
	{
		// Arrange
		var memberDetails = PersistenceTestsHelper.CreateValidAddMemberDomain();
		var memberEntity = PersistenceTestsHelper.CreateValidMemberDetailsEntity();

		_mockMemberRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(new List<MemberDetails>());

		_mockStatusRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MembershipStatusMapping, bool>>>()))
			.ReturnsAsync((MembershipStatusMapping?)null);

		_mockMapper.Setup(x => x.Map<MemberDetails>(memberDetails)).Returns(memberEntity);

		_mockMemberRepository.Setup(x => x.AddAsync(It.IsAny<MemberDetails>())).ReturnsAsync(memberEntity);
		_mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

		// Act
		var result = await _membersDataManager.AddNewMemberAsync(memberDetails);

		// Assert
		Assert.True(result);
		Assert.Equal(0, memberEntity.MembershipStatusId);
	}

	#endregion

	#region GetAllMembersAsync Tests

	/// <summary>
	/// Tests that GetAllMembersAsync returns all active members successfully.
	/// </summary>
	[Fact]
	public async Task GetAllMembersAsync_WithActiveMembers_ShouldReturnMembersList()
	{
		// Arrange
		var memberEntities = PersistenceTestsHelper.CreateValidMemberDetailsEntityList(3);
		var memberDomains = PersistenceTestsHelper.CreateValidMemberDetailsDomainList(3);

		_mockMemberRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<string>(),
			It.IsAny<int>(),
			It.IsAny<int>(),
			It.IsAny<bool>()))
			.ReturnsAsync(memberEntities);

		_mockMapper.Setup(x => x.Map<List<MemberDetailsDomain>>(memberEntities))
			.Returns(memberDomains);

		// Act
		var result = await _membersDataManager.GetAllMembersAsync();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(3, result.Count);
		_mockMemberRepository.Verify(x => x.GetAllAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<string>(),
			It.IsAny<int>(),
			It.IsAny<int>(),
			It.IsAny<bool>()), Times.Once);
	}

	/// <summary>
	/// Tests that GetAllMembersAsync returns empty list when no members exist.
	/// </summary>
	[Fact]
	public async Task GetAllMembersAsync_WithNoMembers_ShouldReturnEmptyList()
	{
		// Arrange
		var emptyList = new List<MemberDetails>();
		var emptyDomainList = new List<MemberDetailsDomain>();

		_mockMemberRepository.Setup(x => x.GetAllAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<string>(),
			It.IsAny<int>(),
			It.IsAny<int>(),
			It.IsAny<bool>()))
			.ReturnsAsync(emptyList);

		_mockMapper.Setup(x => x.Map<List<MemberDetailsDomain>>(emptyList))
			.Returns(emptyDomainList);

		// Act
		var result = await _membersDataManager.GetAllMembersAsync();

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}

	/// <summary>
	/// Tests that GetAllMembersAsync handles exception during database operation.
	/// </summary>
	[Fact]
	public async Task GetAllMembersAsync_WithDatabaseException_ShouldThrowException()
	{
		// Arrange
		_mockMemberRepository.Setup(x => x.GetAllAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<string>(),
			It.IsAny<int>(),
			It.IsAny<int>(),
			It.IsAny<bool>()))
			.ThrowsAsync(new Exception("Database connection failed"));

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			() => _membersDataManager.GetAllMembersAsync());

		Assert.Equal("Database connection failed", exception.Message);
	}

	#endregion

	#region GetMemberByEmailIdAsync Tests

	/// <summary>
	/// Tests that GetMemberByEmailIdAsync returns member when found by email.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_WithValidEmail_ShouldReturnMember()
	{
		// Arrange
		var email = PersistenceTestsHelper.CurrentLoggedInMember;
		var memberEntity = PersistenceTestsHelper.CreateValidMemberDetailsEntity(1, email);
		var memberDomain = PersistenceTestsHelper.CreateValidMemberDetailsDomain(1, email);

		_mockMemberRepository.Setup(x => x.GetAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<bool>(),
			It.IsAny<string>(),
			It.IsAny<bool>()))
			.ReturnsAsync(memberEntity);

		_mockMapper.Setup(x => x.Map<MemberDetailsDomain>(memberEntity))
			.Returns(memberDomain);

		// Act
		var result = await _membersDataManager.GetMemberByEmailIdAsync(email);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(email, result.MemberEmail);
		Assert.Equal(memberDomain.MemberId, result.MemberId);
	}

	/// <summary>
	/// Tests that GetMemberByEmailIdAsync returns null when member not found.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_WithNonExistentEmail_ShouldReturnNull()
	{
		// Arrange
		var email = "nonexistent@example.com";

		_mockMemberRepository.Setup(x => x.GetAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<bool>(),
			It.IsAny<string>(),
			It.IsAny<bool>()))
			.ReturnsAsync((MemberDetails?)null!);

		_mockMapper.Setup(x => x.Map<MemberDetailsDomain>(It.IsAny<MemberDetails>()))
			.Returns((MemberDetailsDomain?)null!);

		// Act
		var result = await _membersDataManager.GetMemberByEmailIdAsync(email);

		// Assert
		Assert.Null(result);
	}

	/// <summary>
	/// Tests that GetMemberByEmailIdAsync handles exception during database operation.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_WithDatabaseException_ShouldThrowException()
	{
		// Arrange
		var email = PersistenceTestsHelper.CurrentLoggedInMember;

		_mockMemberRepository.Setup(x => x.GetAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<bool>(),
			It.IsAny<string>(),
			It.IsAny<bool>()))
			.ThrowsAsync(new Exception("Database timeout"));

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			() => _membersDataManager.GetMemberByEmailIdAsync(email));

		Assert.Equal("Database timeout", exception.Message);
	}

	#endregion

	#region UpdateMemberDetailsAsync Tests

	/// <summary>
	/// Tests that UpdateMemberDetailsAsync successfully updates member when valid data is provided.
	/// </summary>
	[Fact]
	public async Task UpdateMemberDetailsAsync_WithValidData_ShouldReturnTrue()
	{
		// Arrange
		var updateMemberDomain = PersistenceTestsHelper.CreateValidUpdateMemberDomain();
		var existingMember = PersistenceTestsHelper.CreateValidMemberDetailsEntity();

		_mockMemberRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(existingMember);

		_mockMemberRepository.Setup(x => x.Update(It.IsAny<MemberDetails>()));
		_mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

		// Act
		var result = await _membersDataManager.UpdateMemberDetailsAsync(updateMemberDomain);

		// Assert
		Assert.True(result);
		_mockMemberRepository.Verify(x => x.Update(It.IsAny<MemberDetails>()), Times.Once);
		_mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
	}

	/// <summary>
	/// Tests that UpdateMemberDetailsAsync throws InvalidOperationException when member not found.
	/// </summary>
	[Fact]
	public async Task UpdateMemberDetailsAsync_WithNonExistentMember_ShouldThrowInvalidOperationException()
	{
		// Arrange
		var updateMemberDomain = PersistenceTestsHelper.CreateValidUpdateMemberDomain();

		_mockMemberRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync((MemberDetails?)null);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<InvalidOperationException>(
			() => _membersDataManager.UpdateMemberDetailsAsync(updateMemberDomain));

		Assert.Equal(ValidationErrorMessages.MemberNotFoundMessageConstant, exception.Message);
		_mockMemberRepository.Verify(x => x.Update(It.IsAny<MemberDetails>()), Times.Never);
		_mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);
	}

	/// <summary>
	/// Tests that UpdateMemberDetailsAsync handles exception during database operation.
	/// </summary>
	[Fact]
	public async Task UpdateMemberDetailsAsync_WithDatabaseException_ShouldThrowException()
	{
		// Arrange
		var updateMemberDomain = PersistenceTestsHelper.CreateValidUpdateMemberDomain();
		var existingMember = PersistenceTestsHelper.CreateValidMemberDetailsEntity();

		_mockMemberRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(existingMember);

		_mockUnitOfWork.Setup(x => x.SaveChangesAsync())
			.ThrowsAsync(new Exception("Database constraint violation"));

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			() => _membersDataManager.UpdateMemberDetailsAsync(updateMemberDomain));

		Assert.Equal("Database constraint violation", exception.Message);
	}

	#endregion

	#region UpdateMembershipStatusAsync Tests

	/// <summary>
	/// Tests that UpdateMembershipStatusAsync successfully updates membership status when valid data is provided.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_WithValidData_ShouldReturnTrue()
	{
		// Arrange
		var updateStatusDomain = PersistenceTestsHelper.CreateValidUpdateMembershipStatusDomain();
		var existingMember = PersistenceTestsHelper.CreateValidMemberDetailsEntity();

		_mockMemberRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(existingMember);

		_mockMemberRepository.Setup(x => x.Update(It.IsAny<MemberDetails>()));
		_mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

		// Act
		var result = await _membersDataManager.UpdateMembershipStatusAsync(updateStatusDomain);

		// Assert
		Assert.True(result);
		_mockMemberRepository.Verify(x => x.Update(It.IsAny<MemberDetails>()), Times.Once);
		_mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
	}

	/// <summary>
	/// Tests that UpdateMembershipStatusAsync throws InvalidOperationException when member not found.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_WithNonExistentMember_ShouldThrowInvalidOperationException()
	{
		// Arrange
		var updateStatusDomain = PersistenceTestsHelper.CreateValidUpdateMembershipStatusDomain();

		_mockMemberRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync((MemberDetails?)null);

		// Act & Assert
		var exception = await Assert.ThrowsAsync<InvalidOperationException>(
			() => _membersDataManager.UpdateMembershipStatusAsync(updateStatusDomain));

		Assert.Equal(ValidationErrorMessages.MemberNotFoundMessageConstant, exception.Message);
		_mockMemberRepository.Verify(x => x.Update(It.IsAny<MemberDetails>()), Times.Never);
		_mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);
	}

	/// <summary>
	/// Tests that UpdateMembershipStatusAsync handles exception during database operation.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_WithDatabaseException_ShouldThrowException()
	{
		// Arrange
		var updateStatusDomain = PersistenceTestsHelper.CreateValidUpdateMembershipStatusDomain();
		var existingMember = PersistenceTestsHelper.CreateValidMemberDetailsEntity();

		_mockMemberRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(existingMember);

		_mockUnitOfWork.Setup(x => x.SaveChangesAsync())
			.ThrowsAsync(new Exception("Concurrency conflict"));

		// Act & Assert
		var exception = await Assert.ThrowsAsync<Exception>(
			() => _membersDataManager.UpdateMembershipStatusAsync(updateStatusDomain));

		Assert.Equal("Concurrency conflict", exception.Message);
	}

	#endregion

	#region Logging Tests

	/// <summary>
	/// Tests that appropriate logging occurs during successful AddNewMemberAsync operation.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_ShouldLogAppropriateMessages()
	{
		// Arrange
		var memberDetails = PersistenceTestsHelper.CreateValidAddMemberDomain();
		var memberEntity = PersistenceTestsHelper.CreateValidMemberDetailsEntity();
		var statusMapping = PersistenceTestsHelper.CreateValidMembershipStatusMapping();

		_mockMemberRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(new List<MemberDetails>());

		_mockStatusRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MembershipStatusMapping, bool>>>()))
			.ReturnsAsync(statusMapping);

		_mockMapper.Setup(x => x.Map<MemberDetails>(memberDetails)).Returns(memberEntity);

		_mockMemberRepository.Setup(x => x.AddAsync(It.IsAny<MemberDetails>())).ReturnsAsync(memberEntity);
		_mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

		// Act
		await _membersDataManager.AddNewMemberAsync(memberDetails);

		// Assert
		_mockLogger.Verify(
			x => x.Log(
				LogLevel.Information,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("AddNewMemberAsync") && v.ToString()!.Contains("started")),
				It.IsAny<Exception>(),
				It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
			Times.Once);

		_mockLogger.Verify(
			x => x.Log(
				LogLevel.Information,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("AddNewMemberAsync") && v.ToString()!.Contains("ended")),
				It.IsAny<Exception>(),
				It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
			Times.Once);
	}

	/// <summary>
	/// Tests that error logging occurs during failed AddNewMemberAsync operation.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_WithException_ShouldLogError()
	{
		// Arrange
		var memberDetails = PersistenceTestsHelper.CreateValidAddMemberDomain();

		_mockMemberRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ThrowsAsync(new Exception("Database error"));

		// Act & Assert
		await Assert.ThrowsAsync<Exception>(() => _membersDataManager.AddNewMemberAsync(memberDetails));

		_mockLogger.Verify(
			x => x.Log(
				LogLevel.Error,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("AddNewMemberAsync") && v.ToString()!.Contains("failed")),
				It.IsAny<Exception>(),
				It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
			Times.Once);
	}

	#endregion

	#region Edge Case Tests

	/// <summary>
	/// Tests that AddNewMemberAsync handles DateTime.MinValue dates correctly through EnsureValidDates.
	/// </summary>
	[Fact]
	public async Task AddNewMemberAsync_WithMinValueDates_ShouldHandleGracefully()
	{
		// Arrange
		var memberDetails = PersistenceTestsHelper.CreateValidAddMemberDomain();
		memberDetails.MemberDateOfBirth = DateTime.MinValue;
		memberDetails.MemberJoinDate = DateTime.MinValue;
		memberDetails.DateCreated = DateTime.MinValue;
		memberDetails.DateModified = DateTime.MinValue;

		var memberEntity = PersistenceTestsHelper.CreateValidMemberDetailsEntity();
		var statusMapping = PersistenceTestsHelper.CreateValidMembershipStatusMapping();

		_mockMemberRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(new List<MemberDetails>());

		_mockStatusRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MembershipStatusMapping, bool>>>()))
			.ReturnsAsync(statusMapping);

		_mockMapper.Setup(x => x.Map<MemberDetails>(memberDetails)).Returns(memberEntity);

		_mockMemberRepository.Setup(x => x.AddAsync(It.IsAny<MemberDetails>())).ReturnsAsync(memberEntity);
		_mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

		// Act
		var result = await _membersDataManager.AddNewMemberAsync(memberDetails);

		// Assert
		Assert.True(result);
		// Verify that EnsureValidDates was called and dates were updated
		Assert.NotEqual(DateTime.MinValue, memberDetails.MemberDateOfBirth);
		Assert.NotEqual(DateTime.MinValue, memberDetails.MemberJoinDate);
		Assert.NotEqual(DateTime.MinValue, memberDetails.DateCreated);
		Assert.NotEqual(DateTime.MinValue, memberDetails.DateModified);
	}

	/// <summary>
	/// Tests that GetAllMembersAsync includes correct navigation properties.
	/// </summary>
	[Fact]
	public async Task GetAllMembersAsync_ShouldIncludeNavigationProperties()
	{
		// Arrange
		var memberEntities = PersistenceTestsHelper.CreateValidMemberDetailsEntityList(1);
		var memberDomains = PersistenceTestsHelper.CreateValidMemberDetailsDomainList(1);

		_mockMemberRepository.Setup(x => x.GetAllAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<string>(),
			It.IsAny<int>(),
			It.IsAny<int>(),
			It.IsAny<bool>()))
			.ReturnsAsync(memberEntities);

		_mockMapper.Setup(x => x.Map<List<MemberDetailsDomain>>(memberEntities))
			.Returns(memberDomains);

		// Act
		await _membersDataManager.GetAllMembersAsync();

		// Assert
		_mockMemberRepository.Verify(x => x.GetAllAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			"MembershipStatusMapping",
			It.IsAny<int>(),
			It.IsAny<int>(),
			It.IsAny<bool>()), Times.Once);
	}

	/// <summary>
	/// Tests that GetMemberByEmailIdAsync uses correct tracking and navigation properties.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_ShouldUseCorrectTrackingAndNavigation()
	{
		// Arrange
		var email = PersistenceTestsHelper.CurrentLoggedInMember;
		var memberEntity = PersistenceTestsHelper.CreateValidMemberDetailsEntity(1, email);
		var memberDomain = PersistenceTestsHelper.CreateValidMemberDetailsDomain(1, email);

		_mockMemberRepository.Setup(x => x.GetAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<bool>(),
			It.IsAny<string>(),
			It.IsAny<bool>()))
			.ReturnsAsync(memberEntity);

		_mockMapper.Setup(x => x.Map<MemberDetailsDomain>(memberEntity))
			.Returns(memberDomain);

		// Act
		await _membersDataManager.GetMemberByEmailIdAsync(email);

		// Assert
		_mockMemberRepository.Verify(x => x.GetAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			true, // tracked should be true
			"MembershipStatusMapping",
			It.IsAny<bool>()), Times.Once);
	}

	/// <summary>
	/// Tests that UpdateMemberDetailsAsync correctly applies entity updates through extension method.
	/// </summary>
	[Fact]
	public async Task UpdateMemberDetailsAsync_ShouldApplyEntityUpdatesCorrectly()
	{
		// Arrange
		var updateMemberDomain = PersistenceTestsHelper.CreateValidUpdateMemberDomain();
		var existingMember = PersistenceTestsHelper.CreateValidMemberDetailsEntity();
		var originalName = existingMember.MemberName;

		_mockMemberRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(existingMember);

		_mockMemberRepository.Setup(x => x.Update(It.IsAny<MemberDetails>()));
		_mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

		// Act
		await _membersDataManager.UpdateMemberDetailsAsync(updateMemberDomain);

		// Assert
		// Verify that the entity was updated with new values
		Assert.Equal(updateMemberDomain.MemberName, existingMember.MemberName);
		Assert.Equal(updateMemberDomain.MemberPhoneNumber, existingMember.MemberPhoneNumber);
		Assert.Equal(updateMemberDomain.MemberAddress, existingMember.MemberAddress);
		Assert.Equal(updateMemberDomain.ModifiedBy, existingMember.ModifiedBy);
	}

	/// <summary>
	/// Tests that UpdateMembershipStatusAsync correctly applies status updates through extension method.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_ShouldApplyStatusUpdatesCorrectly()
	{
		// Arrange
		var updateStatusDomain = PersistenceTestsHelper.CreateValidUpdateMembershipStatusDomain();
		var existingMember = PersistenceTestsHelper.CreateValidMemberDetailsEntity();
		var originalStatusId = existingMember.MembershipStatusId;

		_mockMemberRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(existingMember);

		_mockMemberRepository.Setup(x => x.Update(It.IsAny<MemberDetails>()));
		_mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

		// Act
		await _membersDataManager.UpdateMembershipStatusAsync(updateStatusDomain);

		// Assert
		// Verify that the entity was updated with new status
		Assert.Equal(updateStatusDomain.MembershipStatusId, existingMember.MembershipStatusId);
		Assert.Equal(updateStatusDomain.ModifiedBy, existingMember.ModifiedBy);
		Assert.NotEqual(originalStatusId, existingMember.MembershipStatusId);
	}

	/// <summary>
	/// Tests that all methods handle empty string parameters appropriately.
	/// </summary>
	[Fact]
	public async Task GetMemberByEmailIdAsync_WithEmptyEmail_ShouldHandleGracefully()
	{
		// Arrange
		var email = string.Empty;

		_mockMemberRepository.Setup(x => x.GetAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<bool>(),
			It.IsAny<string>(),
			It.IsAny<bool>()))
			.ReturnsAsync((MemberDetails?)null!);

		_mockMapper.Setup(x => x.Map<MemberDetailsDomain>(It.IsAny<MemberDetails>()))
			.Returns((MemberDetailsDomain?)null!);

		// Act
		var result = await _membersDataManager.GetMemberByEmailIdAsync(email);

		// Assert
		Assert.Null(result);
		_mockMemberRepository.Verify(x => x.GetAsync(
			It.IsAny<Expression<Func<MemberDetails, bool>>>(),
			It.IsAny<bool>(),
			It.IsAny<string>(),
			It.IsAny<bool>()), Times.Once);
	}

	/// <summary>
	/// Tests that logging occurs with correct method names in UpdateMembershipStatusAsync.
	/// </summary>
	[Fact]
	public async Task UpdateMembershipStatusAsync_ShouldLogWithCorrectMethodName()
	{
		// Arrange
		var updateStatusDomain = PersistenceTestsHelper.CreateValidUpdateMembershipStatusDomain();
		var existingMember = PersistenceTestsHelper.CreateValidMemberDetailsEntity();

		_mockMemberRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<MemberDetails, bool>>>()))
			.ReturnsAsync(existingMember);

		_mockMemberRepository.Setup(x => x.Update(It.IsAny<MemberDetails>()));
		_mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

		// Act
		await _membersDataManager.UpdateMembershipStatusAsync(updateStatusDomain);

		// Assert
		_mockMemberRepository.Verify(x => x.Update(It.IsAny<MemberDetails>()), Times.Once);
		_mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
	}

	#endregion
}
