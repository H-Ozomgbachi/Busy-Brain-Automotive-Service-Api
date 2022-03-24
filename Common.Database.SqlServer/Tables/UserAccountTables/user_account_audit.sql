CREATE TABLE [dbo].[user_account_audit]
(
	[id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [user_id] UNIQUEIDENTIFIER NOT NULL,
    [created_utc] DATETIME2 NOT NULL,
    [action] NVARCHAR(15) NOT NULL,
    [action_body] NVARCHAR(Max) NOT NULL,
    [changed_by_user_id] UNIQUEIDENTIFIER NOT NULL     
)