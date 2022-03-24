CREATE TABLE [dbo].[failure_component]
(
	[id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[title] NVARCHAR(256) NOT NULL,
	[assembly_or_system_name] NVARCHAR(256) NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE(),
	[modified_by] UNIQUEIDENTIFIER NOT NULL
)
