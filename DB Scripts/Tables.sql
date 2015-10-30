CREATE DATABASE WebAnalytics
GO
USE WebAnalytics
GO
CREATE TABLE ApplicationMaster
(
	AppID INT IDENTITY(1,1),
	AppName VARCHAR(20)
)
GO

CREATE TABLE PageStats
(
	AppID INT,
	PageID VARCHAR(50),
	PageTitle VARCHAR(500),
	RefererURL VARCHAR(2000),
	SessionID VARCHAR(200),
	UserName VARCHAR(100),
	KeyWords VARCHAR(250),
	HitDateTime	DATETIME,
	LoadTime INT, --MilliSeconds
	IdleTime INT, --Milliseconds
	Action VARCHAR(50), --PageLoad, ItemClick
	ActionName VARCHAR(250), --ItemName
	ClientIP VARCHAR(100), 
	FullURL VARCHAR(2000),
	Region VARCHAR(250),
	City VARCHAR(250),
	Country VARCHAR(250),
	Language VARCHAR(250),
	Browser VARCHAR(1000)
)
GO


INSERT INTO PageStats(AppID,PageID) values(12,14)
select * from Pagestats
delete from Pagestats


ALTER TABLE PageStats
	ALTER COLUMN PageID VARCHAR(50)