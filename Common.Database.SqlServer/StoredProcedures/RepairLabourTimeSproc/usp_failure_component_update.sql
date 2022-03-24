CREATE PROCEDURE [dbo].[usp_failure_component_update]
	@id int,
	@title nvarchar(256),
	@assembly_or_system_name nvarchar(256),
	@created_at datetime,
	@modified_by uniqueidentifier
AS
BEGIN
	UPDATE failure_component
	SET
		[title] = @title,
		[assembly_or_system_name] = @assembly_or_system_name,
		[created_at] = @created_at,
		[modified_by] = @modified_by
	WHERE id = @id
END