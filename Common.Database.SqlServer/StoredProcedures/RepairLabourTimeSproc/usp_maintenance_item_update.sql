CREATE PROCEDURE [dbo].[usp_maintenance_item_update]
	@id int,
	@title nvarchar(max),
	@code nvarchar(50),
	@labour_time_hours int,
	@truck_model nvarchar(100),
	@cost_per_hour decimal(10,2),
	@failure_component_id int,
	@created_at datetime,
	@modified_at datetime,
	@modified_by uniqueidentifier
AS
BEGIN
	UPDATE maintenance_item
	SET
		[title] = @title,
		[code] = @code,
		[labour_time_hours] = @labour_time_hours,
		[truck_model] = @truck_model,
		[cost_per_hour] = @cost_per_hour,
		[failure_component_id] = @failure_component_id,
		[created_at] = @created_at,
		[modified_at] = @modified_at,
		[modified_by] = @modified_by
	WHERE id = @id
END