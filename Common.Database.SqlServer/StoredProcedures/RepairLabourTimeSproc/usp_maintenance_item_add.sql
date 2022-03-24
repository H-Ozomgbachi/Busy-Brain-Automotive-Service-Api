CREATE PROCEDURE [dbo].[usp_maintenance_item_add]
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
	INSERT INTO maintenance_item (title, code, labour_time_hours, truck_model, cost_per_hour, failure_component_id, created_at, modified_at, modified_by)
	OUTPUT inserted.id
	VALUES (@title, @code, @labour_time_hours, @truck_model, @cost_per_hour, @failure_component_id, @created_at, @modified_at, @modified_by)
END