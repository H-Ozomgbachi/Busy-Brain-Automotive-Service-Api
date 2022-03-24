CREATE PROCEDURE [dbo].[usp_get_maintenance_item_by_code]
	@code nvarchar(50)
AS
BEGIN
	SELECT * FROM maintenance_item
	WHERE code = @code
END