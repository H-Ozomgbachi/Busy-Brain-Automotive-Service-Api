CREATE PROCEDURE [dbo].[usp_get_maintenance_items_by_failure_component]
	@failure_component_id int
AS
BEGIN
	SELECT id, title, code, labour_time_hours, truck_model, cost_per_hour, failure_component_id FROM maintenance_item
	WHERE failure_component_id = @failure_component_id

END