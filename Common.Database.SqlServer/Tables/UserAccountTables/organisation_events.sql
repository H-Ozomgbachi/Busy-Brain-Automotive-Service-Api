CREATE TABLE [dbo].[organisation_events]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [event_type] NVARCHAR(Max) NOT NULL,
    [created] DATETIME2 NOT NULL,
    [event_body] NVARCHAR(Max) NULL,
    [org_id] int NOT NULL,
    [changed_by_user_id] UNIQUEIDENTIFIER NOT NULL, 
	[account_type] NVARCHAR(Max) NOT NULL,
)