/*
	Author: Debanjan Paul
	Purpose: Adds an error log to the ErrorLog table.
	DateCreated: 23-July-2025
*/

CREATE PROCEDURE [dbo].[SP_InsertErrorLog]
(
	@ErrorMessage NVARCHAR(MAX),
	@ErrorProcedure NVARCHAR(MAX),
	@ErrorLine NVARCHAR(MAX)
)
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
	INSERT INTO dbo.ErrorLog ([ErrorMessage], [ErrorProcedure], [ErrorLine])
	VALUES (@ErrorMessage, @ErrorProcedure, @ErrorLine)

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END TRY

BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;
END CATCH
END