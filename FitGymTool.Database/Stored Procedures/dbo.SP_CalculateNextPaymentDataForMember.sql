-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Author			|		Modification Date		|					Description														|
-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Debanjan Paul	|		26-07-2025				|	Calculates the next payment data for member										|
-------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[SP_CalculateNextPaymentDataForMember]
(
	@MemberEmailId NVARCHAR(MAX)
)
AS
BEGIN
SET NOCOUNT ON;

BEGIN TRANSACTION;
BEGIN TRY

	
	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;

END TRY

BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;

	DECLARE 
		@ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE(),
		@ErrorProcedure NVARCHAR(MAX) = ERROR_PROCEDURE(),
		@ErrorLine NVARCHAR(MAX) = CAST(ERROR_LINE() AS NVARCHAR(MAX))

	EXEC [dbo].[SP_InsertErrorLog] @ErrorMessage, @ErrorProcedure, @ErrorLine

END CATCH

END
