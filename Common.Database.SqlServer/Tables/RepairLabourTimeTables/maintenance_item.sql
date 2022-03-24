CREATE TABLE [dbo].[maintenance_item]
(
	[id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[title] NVARCHAR(MAX) NOT NULL,
	[code] NVARCHAR(50) NOT NULL,
	[labour_time_hours] INT NOT NULL,
	[truck_model] NVARCHAR(100) NOT NULL,
	[cost_per_hour] DECIMAL(10,2) NOT NULL,
	[failure_component_id] INT NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE(),
	[modified_at] DATETIME NOT NULL DEFAULT GETDATE(),
	[modified_by] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [FK_maintenance_item_failure_component] FOREIGN KEY (failure_component_id) REFERENCES failure_component(id) ON DELETE CASCADE
)
