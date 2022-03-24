CREATE PROCEDURE [dbo].[usp_get_maintenance_item]
	@failure_component_id int,
	@id int
AS
BEGIN
	SELECT * FROM maintenance_item
	WHERE failure_component_id = @failure_component_id and id = @id
END