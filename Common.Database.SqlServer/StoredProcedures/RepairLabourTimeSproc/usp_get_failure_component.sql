CREATE PROCEDURE [dbo].[usp_get_failure_component]
	@id int
AS
BEGIN
	SELECT * FROM failure_component
	WHERE id = @id
END
