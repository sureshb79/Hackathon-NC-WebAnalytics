drop proc [dbo].[AddPageStat]
GO
CREATE PROCEDURE [dbo].[AddPageStat]
	@AppID int = NULL,
	@PageID  VARCHAR(50) = NULL,
	@PageTitle VARCHAR(500),
	@RefererURL VARCHAR(2000)

AS
BEGIN
	IF @AppID IS NULL OR @PageID IS NULL
		RETURN 0
	INSERT INTO PageStats(AppID,PageID,PageTitle,RefererURL) values(@AppID,@PageID,@PageTitle,@RefererURL)
	RETURN 0
END

