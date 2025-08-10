-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Author			|		Modification Date		|					                Description						                |
-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Debanjan Paul	|		10-08-2025				|	            Updates the member's membership fees status 	                    |
-------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[SP_UpdateMemberFeesData]
(
    @UpdateMemberFeesData [dbo].[UpdateMemberFeesTableType] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate input parameters
    IF NOT EXISTS (SELECT 1 FROM @UpdateMemberFeesData)
    BEGIN
        RAISERROR('No member data provided', 16, 1);
        RETURN;
    END
    
    DECLARE @TotalUpdateCount INT = 0;
    DECLARE @PaidStatusId INT;
    
    -- Get the PaidStatusId once for all operations
    SELECT @PaidStatusId = fps.Id
    FROM [dbo].[FeesPaymentStatusMapping] fps (NOLOCK)
    WHERE fps.[IsActive] = 1 
        AND fps.[StatusName] = 'Paid';
    
    -- Validate PaidStatusId was found
    IF @PaidStatusId IS NULL
    BEGIN
        RAISERROR ('Paid status mapping not found', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Update payment history for all valid members in the table parameter
        UPDATE fph
        SET [PaymentStatusId] = @PaidStatusId, 
            [DateModified] = GETDATE(), 
            [ModifiedBy] = umfd.ModifiedBy
        FROM [dbo].[FeesPaymentHistory] fph
        INNER JOIN [dbo].[MemberDetails] md (NOLOCK) ON fph.MemberId = md.MemberId
        INNER JOIN @UpdateMemberFeesData umfd ON md.MemberEmail = umfd.MemberEmail
            AND fph.FromDate = umfd.FromDate
            AND fph.ToDate = umfd.ToDate
            AND fph.Amount = umfd.Amount
        WHERE md.[IsActive] = 1 
            AND fph.[PaymentStatusId] = 2  
            AND fph.[IsActive] = 1;
        
        SET @TotalUpdateCount = @@ROWCOUNT;
        
        -- Process next payment calculation for each updated member
        IF @TotalUpdateCount > 0
        BEGIN
            DECLARE @MemberEmail NVARCHAR(255);
            DECLARE member_cursor CURSOR FOR
            SELECT DISTINCT umfd.MemberEmail
            FROM @UpdateMemberFeesData umfd
            INNER JOIN [dbo].[MemberDetails] md (NOLOCK) ON md.MemberEmail = umfd.MemberEmail
            WHERE md.[IsActive] = 1;
            
            OPEN member_cursor;
            FETCH NEXT FROM member_cursor INTO @MemberEmail;
            
            WHILE @@FETCH_STATUS = 0
            BEGIN
                EXEC [dbo].[SP_CalculateNextPaymentDataForMember] @MemberEmail;
                FETCH NEXT FROM member_cursor INTO @MemberEmail;
            END
            
            CLOSE member_cursor;
            DEALLOCATE member_cursor;
        END
        ELSE
        BEGIN
            -- Log when no records were updated for audit purposes
            PRINT 'No payment records found to update for the provided member data';
        END

        COMMIT TRANSACTION;
        
        -- Return success indicator
        SELECT @TotalUpdateCount AS RecordsUpdated;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE
            @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE(),
            @ErrorProcedure NVARCHAR(MAX) = ISNULL(ERROR_PROCEDURE(), 'SP_UpdateMemberFeesData'),
            @ErrorLine INT = ERROR_LINE(),
            @ErrorSeverity INT = ERROR_SEVERITY(),
            @ErrorState INT = ERROR_STATE();

        -- Log error with more details
        EXEC [dbo].[SP_InsertErrorLog] @ErrorMessage, @ErrorProcedure, @ErrorLine;
        
        -- Re-throw with original error info
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END