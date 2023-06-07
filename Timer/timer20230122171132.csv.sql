/*
Generated: 2023-01-24 08:31:16 
From: C:\Users\perh\source\Test\FetchJira\Timer\timer20230122171132.csv
Analyzed 152 rows

username            : String, HasNull: False, MinLength: 3, MaxLength: 7
email               : String, HasNull: False, MinLength: 12, MaxLength: 16
timeSpentSeconds    : Int, HasNull: False, MinValue: 1800, MaxValue: 48600
issueId             : Int, HasNull: False, MinValue: 51343, MaxValue: 211247
issueKey            : String, HasNull: False, MinLength: 6, MaxLength: 11
started             : DateTime, HasNull: False, MinLength: 19, MaxLength: 19
id                  : Int, HasNull: False, MinValue: 342122, MaxValue: 346420
caseNumber          : String, HasNull: True, MinLength: 19, MaxLength: 49
taskNumber          : String, HasNull: True, MinLength: 1, MaxLength: 30
summary             : String, HasNull: False, MinLength: 10, MaxLength: 67
labels              : String, HasNull: True, MinLength: 5, MaxLength: 22
projectkey          : String, HasNull: False, MinLength: 3, MaxLength: 6
projectname         : String, HasNull: False, MinLength: 3, MaxLength: 23
parentissuekey      : String, HasNull: True, MinLength: 6, MaxLength: 9
parentissuetypename : String, HasNull: True, MinLength: 4, MaxLength: 5
parentissuesummary  : String, HasNull: True, MinLength: 8, MaxLength: 57
createdatetime      : DateTime, HasNull: False, MinLength: 19, MaxLength: 19
*/

IF OBJECT_ID('timer', 'U') IS NOT NULL DROP TABLE [timer];
CREATE TABLE [timer](
	[username] [char](7) NOT NULL,
	[email] [varchar](20) NOT NULL,
	[timeSpentSeconds] [int] NOT NULL,
	[issueId] [int] NOT NULL,
	[issueKey] [varchar](20) NOT NULL,
	[started] [datetime] NOT NULL,
	[id] [int] NOT NULL,
	[caseNumber] [varchar](60) NULL,
	[taskNumber] [varchar](40) NULL,
	[summary] [varchar](80) NOT NULL,
	[labels] [varchar](30) NULL,
	[projectkey] [char](6) NOT NULL,
	[projectname] [varchar](30) NOT NULL,
	[parentissuekey] [char](9) NULL,
	[parentissuetypename] [char](5) NULL,
	[parentissuesummary] [varchar](70) NULL,
	[createdatetime] [datetime] NOT NULL);
