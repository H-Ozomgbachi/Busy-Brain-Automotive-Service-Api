CREATE PROCEDURE [dbo].[usp_get_maintenance_items_by_multiple_failure_component]
	@failure_component_ids nvarchar(max)
AS
BEGIN
	DECLARE @Sql NVARCHAR(MAX)
	SET @failure_component_ids = REPLACE(@failure_component_ids, ',', ''', ''')
	SET @Sql = 'SELECT id, title, code, labour_time_hours, truck_model, cost_per_hour, failure_component_id FROM maintenance_item
	WHERE failure_component_id IN 
	(
		''' + @failure_component_ids + '''
	)'
	EXEC(@Sql)
END