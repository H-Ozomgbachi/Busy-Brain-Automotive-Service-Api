CREATE PROCEDURE [dbo].[usp_maintenance_item_delete]
	@failure_component_id int,
	@id int
AS
BEGIN
	DELETE FROM maintenance_item
	WHERE failure_component_id = @failure_component_id and id = @id
END