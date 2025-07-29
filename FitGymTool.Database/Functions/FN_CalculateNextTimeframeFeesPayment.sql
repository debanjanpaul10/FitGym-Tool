CREATE FUNCTION [dbo].[FN_CalculateNextTimeframeFeesPayment]
(
	@MemberEmailId NVARCHAR(MAX)
)
RETURNS @returntable TABLE
(
	FromDate DATETIME,
	ToDate DATETIME
)
AS
BEGIN
	DECLARE @NextFromDate DATETIME;
	DECLARE @FeesDurationId INT;
	
	SELECT TOP 1 
		@NextFromDate = DATEADD(DAY, 1, FPH.ToDate),
		@FeesDurationId = MFPD.FeesDurationId
	FROM dbo.MemberFeesPaymentDurationMapping MFPD
		INNER JOIN dbo.MemberDetails (NOLOCK) MD ON MD.MemberId = MFPD.MemberId AND MD.MemberEmail = MFPD.MemberEmail AND MD.IsActive = 1
		INNER JOIN dbo.FeesPaymentHistory (NOLOCK) FPH ON FPH.MemberId = MFPD.MemberId AND FPH.IsActive = 1
	WHERE MFPD.IsActive = 1 AND MFPD.MemberEmail = @MemberEmailId
	ORDER BY FPH.ToDate DESC;

	IF @NextFromDate IS NOT NULL
	BEGIN
		INSERT @returntable (FromDate, ToDate)
		SELECT 
			@NextFromDate,
			DATEADD(MONTH, 
				CASE @FeesDurationId 
					WHEN 1 THEN 1
					WHEN 2 THEN 3
					WHEN 3 THEN 6
					WHEN 4 THEN 12
					ELSE 1
				END, 
				@NextFromDate
			);
	END

	RETURN;
END
