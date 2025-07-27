-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Author			|		Modification Date		|					                Description						                |
-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Debanjan Paul	|		27-07-2025				|	            Adds the record for a new member onboarded  	                    |
-------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[SP_AddNewMemberData]
(
    @MemberEmail NVARCHAR(MAX),
    @MemberName NVARCHAR(MAX),
    @MemberPhoneNumber NVARCHAR(MAX),
    @MemberAddress NVARCHAR(MAX),
    @MemberGender NVARCHAR(10),
    @MemberDateOfBirth DATE,
    @MemberJoinDate DATE = NULL,
    @FeesDurationTypeName NVARCHAR(20),
    @CreatedBy NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @ActiveMembershipStatusId INT, @MemberId INT, @FeesDurationId INT;

        SELECT @ActiveMembershipStatusId = Id
        FROM [dbo].[MembershipStatusMapping]
        WHERE [IsActive]=1 AND [StatusName]='Active';

        SELECT @FeesDurationId = Id
        FROM [dbo].[FeesDurationMapping]
        WHERE [IsActive]=1 AND [DurationTypeName]=@FeesDurationTypeName;

        INSERT INTO [dbo].[MemberDetails] (
            [MemberName], [MemberEmail], [MemberPhoneNumber], [MemberAddress], [MemberDateOfBirth], [MemberGender],
            [MemberJoinDate], [MembershipStatusId], [MemberGuid], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]
        )
        VALUES (
            @MemberName, @MemberEmail, @MemberPhoneNumber, @MemberAddress, @MemberDateOfBirth, @MemberGender,
            ISNULL(@MemberJoinDate, GETUTCDATE()), @ActiveMembershipStatusId, NEWID(), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy
        );

        SET @MemberId = SCOPE_IDENTITY();

        INSERT INTO [dbo].[MemberFeesPaymentDurationMapping] (
            [MemberId], [MemberEmail], [FeesDurationId], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]
        )
        VALUES (
            @MemberId, @MemberEmail, @FeesDurationId, 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE
            @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE(),
            @ErrorProcedure NVARCHAR(MAX) = ERROR_PROCEDURE(),
            @ErrorLine NVARCHAR(MAX) = CAST(ERROR_LINE() AS NVARCHAR(MAX));

        EXEC [dbo].[SP_InsertErrorLog] @ErrorMessage, @ErrorProcedure, @ErrorLine;
    END CATCH
END