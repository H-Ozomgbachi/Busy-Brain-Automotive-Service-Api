CREATE PROCEDURE [dbo].[usp_failure_components_count]

AS
BEGIN
	SELECT COUNT(id) as total_count FROM failure_component
END