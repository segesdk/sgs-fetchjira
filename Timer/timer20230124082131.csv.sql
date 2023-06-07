/*
Generated: 2023-01-24 08:31:19 
From: C:\Users\perh\source\Test\FetchJira\Timer\timer20230124082131.csv
Analyzed 33 rows

username            : String, HasNull: False, MinLength: 3, MaxLength: 7
email               : String, HasNull: False, MinLength: 12, MaxLength: 16
timeSpentSeconds    : Int, HasNull: False, MinValue: 1800, MaxValue: 27000
issueId             : Int, HasNull: False, MinValue: 54507, MaxValue: 211258
issueKey            : String, HasNull: False, MinLength: 7, MaxLength: 8
started             : DateTime, HasNull: False, MinLength: 19, MaxLength: 19
id                  : Int, HasNull: False, MinValue: 346419, MaxValue: 346741
caseNumber          : String, HasNull: True, MinLength: 4, MaxLength: 35
taskNumber          : String, HasNull: True, MinLength: 3, MaxLength: 23
summary             : String, HasNull: False, MinLength: 10, MaxLength: 72
labels              : String, HasNull: True, MinLength: 5, MaxLength: 22
projectkey          : String, HasNull: False, MinLength: 3, MaxLength: 4
projectname         : String, HasNull: False, MinLength: 6, MaxLength: 23
parentissuekey      : String, HasNull: True, MinLength: 6, MaxLength: 8
parentissuetypename : String, HasNull: True, MinLength: 4, MaxLength: 5
parentissuesummary  : String, HasNull: True, MinLength: 8, MaxLength: 72
createdatetime      : DateTime, HasNull: False, MinLength: 19, MaxLength: 19
*/

IF OBJECT_ID('timerupdate', 'U') IS NOT NULL DROP TABLE [timerupdate];
CREATE TABLE [timerupdate](
	[username] [char](7) NOT NULL,
	[email] [varchar](20) NOT NULL,
	[timeSpentSeconds] [smallint] NOT NULL,
	[issueId] [int] NOT NULL,
	[issueKey] [char](8) NOT NULL,
	[started] [datetime] NOT NULL,
	[id] [int] NOT NULL,
	[caseNumber] [varchar](40) NULL,
	[taskNumber] [varchar](30) NULL,
	[summary] [varchar](80) NOT NULL,
	[labels] [varchar](30) NULL,
	[projectkey] [char](4) NOT NULL,
	[projectname] [varchar](30) NOT NULL,
	[parentissuekey] [char](8) NULL,
	[parentissuetypename] [char](5) NULL,
	[parentissuesummary] [varchar](80) NULL,
	[createdatetime] [datetime] NOT NULL);
