-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Author			|		Modification Date		|					                Description						                |
-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Debanjan Paul	|		27-07-2025				|	            Adds the record for a new member onboarded  	                    |
-------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[SP_AddNewMemberData]
(
    @NewMemberData [dbo].[AddNewMemberTableType] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate input data exists
    IF NOT EXISTS (SELECT 1 FROM @NewMemberData)
    BEGIN
        RAISERROR('No member data provided', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @CurrentDateTime DATETIME2 = GETUTCDATE();
        DECLARE @MemberId INT;

        -- Get required lookup values with validation in single queries
        DECLARE @ActiveMembershipStatusId INT, @FeesDurationId INT, @DuePaymentStatusId INT;
        
        SELECT @ActiveMembershipStatusId = Id
        FROM [dbo].[MembershipStatusMapping] WITH (READUNCOMMITTED)
        WHERE [IsActive] = 1 AND [StatusName] = 'Active';

        IF @ActiveMembershipStatusId IS NULL
        BEGIN
            RAISERROR('Active membership status not found', 16, 1);
            RETURN;
        END

        SELECT @FeesDurationId = FDM.Id
        FROM [dbo].[FeesDurationMapping] FDM WITH (READUNCOMMITTED)
            INNER JOIN @NewMemberData NMD ON NMD.FeesDurationTypeName = FDM.DurationTypeName
        WHERE FDM.[IsActive] = 1;

        IF @FeesDurationId IS NULL
        BEGIN
            RAISERROR('Invalid fees duration type provided', 16, 1);
            RETURN;
        END

        SELECT @DuePaymentStatusId = Id
        FROM [dbo].[FeesPaymentStatusMapping] WITH (READUNCOMMITTED)
        WHERE [StatusName] = 'Due' AND [IsActive] = 1;

        IF @DuePaymentStatusId IS NULL
        BEGIN
            RAISERROR('Due payment status not found', 16, 1);
            RETURN;
        END

        -- Check for duplicate email
        IF EXISTS (
            SELECT 1 
            FROM [dbo].[MemberDetails] MD WITH (READUNCOMMITTED)
                INNER JOIN @NewMemberData NMD ON MD.MemberEmail = NMD.MemberEmail
            WHERE MD.[IsActive] = 1
        )
        BEGIN
            RAISERROR('Member with this email already exists', 16, 1);
            RETURN;
        END

        -- Insert member details
        INSERT INTO [dbo].[MemberDetails] (
            [MemberName], [MemberEmail], [MemberPhoneNumber], [MemberAddress], 
            [MemberDateOfBirth], [MemberGender], [MemberJoinDate], [MembershipStatusId], 
            [MemberGuid], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]
        )
        SELECT
            NMD.MemberName, NMD.MemberEmail, NMD.MemberPhoneNumber, NMD.MemberAddress,
            NMD.MemberDateOfBirth, NMD.MemberGender, ISNULL(NMD.MemberJoinDate, @CurrentDateTime),
            @ActiveMembershipStatusId, NEWID(), 1, @CurrentDateTime, NMD.CreatedBy, @CurrentDateTime, NMD.CreatedBy
        FROM @NewMemberData NMD;

        SET @MemberId = SCOPE_IDENTITY();

        -- Insert fees payment duration mapping
        INSERT INTO [dbo].[MemberFeesPaymentDurationMapping] (
            [MemberId], [MemberEmail], [FeesDurationId], [IsActive], 
            [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]
        )
        SELECT 
            @MemberId, NMD.MemberEmail, @FeesDurationId, 1, 
            @CurrentDateTime, NMD.CreatedBy, @CurrentDateTime, NMD.CreatedBy
        FROM @NewMemberData NMD;

        -- Insert fees payment history with optimized date calculation
        INSERT INTO [dbo].[FeesPaymentHistory] (
            [MemberGuid], [MemberId], [Amount], [PaymentStatusId], [IsActive], 
            [FromDate], [ToDate], [FeesDurationId], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]
        )
        SELECT 
            MD.[MemberGuid], @MemberId, FS.[FeesAmount], @DuePaymentStatusId, 1, 
            ISNULL(NMD.[MemberJoinDate], @CurrentDateTime),
            CASE 
                WHEN NMD.[FeesDurationTypeName] = 'Monthly' THEN DATEADD(MONTH, 1, ISNULL(NMD.[MemberJoinDate], @CurrentDateTime))
                WHEN NMD.[FeesDurationTypeName] = 'Quarterly' THEN DATEADD(MONTH, 3, ISNULL(NMD.[MemberJoinDate], @CurrentDateTime))
                WHEN NMD.[FeesDurationTypeName] = 'Half-Yearly' THEN DATEADD(MONTH, 6, ISNULL(NMD.[MemberJoinDate], @CurrentDateTime))
                WHEN NMD.[FeesDurationTypeName] = 'Annually' THEN DATEADD(YEAR, 1, ISNULL(NMD.[MemberJoinDate], @CurrentDateTime))
            END,
            @FeesDurationId, @CurrentDateTime, NMD.[CreatedBy], @CurrentDateTime, NMD.[CreatedBy]
        FROM @NewMemberData NMD
            INNER JOIN [dbo].[MemberDetails] MD WITH (READUNCOMMITTED) ON MD.[MemberId] = @MemberId
            INNER JOIN [dbo].[FeesStructure] FS WITH (READUNCOMMITTED) ON FS.[FeesDurationId] = @FeesDurationId AND FS.[IsActive] = 1;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE
            @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE(),
            @ErrorProcedure NVARCHAR(MAX) = ERROR_PROCEDURE(),
            @ErrorLine INT = ERROR_LINE(),
            @ErrorSeverity INT = ERROR_SEVERITY(),
            @ErrorState INT = ERROR_STATE();

        -- Log error with more details
        EXEC [dbo].[SP_InsertErrorLog] @ErrorMessage, @ErrorProcedure, @ErrorLine;
        
        -- Re-throw with original error info
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END