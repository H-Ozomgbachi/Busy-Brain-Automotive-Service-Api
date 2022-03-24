CREATE PROCEDURE [dbo].[usp_failure_component_delete]
	@id int
AS
BEGIN
	DELETE FROM failure_component
	WHERE id = @id
END