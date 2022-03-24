CREATE PROCEDURE [dbo].[usp_failure_component_add]
	@title nvarchar(256),
	@assembly_or_system_name nvarchar(256),
	@created_at datetime,
	@modified_by uniqueidentifier
AS
BEGIN
	INSERT INTO failure_component (title, assembly_or_system_name, created_at, modified_by)
	OUTPUT inserted.id
	VALUES(@title, @assembly_or_system_name, @created_at, @modified_by)
END