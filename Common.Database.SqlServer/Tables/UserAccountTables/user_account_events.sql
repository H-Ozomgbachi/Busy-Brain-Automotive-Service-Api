CREATE TABLE [dbo].[user_account_event]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [event_type] NVARCHAR(Max) NOT NULL,
    [created] DATETIME2 NOT NULL,
    [event_body] NVARCHAR(Max) NULL,
    [user_id] UNIQUEIDENTIFIER NOT NULL,
    [changed_by_user_id] UNIQUEIDENTIFIER NOT NULL, 
    
)