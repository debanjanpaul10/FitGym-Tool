CREATE FUNCTION [dbo].[FN_CalculateNextTimeframeFeesPayment]
(
	@MemberId INT
)
RETURNS @returntable TABLE
(
	FromDate DATETIME,
	ToDate DATETIME
)
AS
BEGIN
	INSERT @returntable
	SELECT 
		DATEADD(DAY, 1, FPH.ToDate) AS FromDate,
		CASE WHEN MFPD.FeesDurationId = 1 THEN DATEADD(MONTH, 1, DATEADD(DAY, 1, FPH.ToDate))
			WHEN MFPD.FeesDurationId = 2 THEN DATEADD(MONTH, 3, DATEADD(DAY, 1, FPH.ToDate))
			WHEN MFPD.FeesDurationId = 3 THEN DATEADD(MONTH, 6, DATEADD(DAY, 1, FPH.ToDate))
			WHEN MFPD.FeesDurationId = 4 THEN DATEADD(MONTH, 12, DATEADD(DAY, 1, FPH.ToDate))
		END AS ToDate
	FROM dbo.MemberFeesPaymentDurationMapping MFPD
		INNER JOIN dbo.MemberDetails MD 
			ON MD.MemberId = MFPD.MemberId 
			AND MD.MemberEmail = MFPD.MemberEmail 
			AND MD.IsActive = 1
		INNER JOIN dbo.FeesPaymentHistory FPH 
			ON FPH.MemberId = MFPD.MemberId 
			AND FPH.IsActive = 1
	WHERE MFPD.IsActive = 1 AND MFPD.MemberId=@MemberId
		AND FPH.ToDate = (
			SELECT MAX(FPH2.ToDate)
			FROM dbo.FeesPaymentHistory FPH2
			INNER JOIN dbo.MemberFeesPaymentDurationMapping MFPD2 
				ON FPH2.MemberId = MFPD2.MemberId
			WHERE FPH2.IsActive = 1 
				AND MFPD2.IsActive = 1
				AND MFPD2.Memberemail = MFPD.Memberemail
		) ORDER BY MFPD.Memberemail

	RETURN

END
