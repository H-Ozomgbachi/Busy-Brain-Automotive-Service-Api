CREATE TABLE [dbo].[organisation]
(
    [id] INT IDENTITY (1, 1) NOT NULL,
	[unique_id] UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL, 
    [name] NVARCHAR(256) NOT NULL,      
    [contact_email] NVARCHAR(300) NOT NULL, 
    [phone] NVARCHAR(50) NOT NULL, 
    [report_email] NVARCHAR(300) NOT NULL,
    [created_utc] DATETIME2 NOT NULL  DEFAULT GETUTCDATE(), 
    [last_modified_utc] DATETIME2 NULL DEFAULT GETUTCDATE(), 
	[account_type] NVARCHAR(300) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
)