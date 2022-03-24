
CREATE PROCEDURE [dbo].[usp_is_tracker_on_active_shipment]
    @tracker_id int	
    
AS
SELECT CASE WHEN EXISTS (
    SELECT Top 1 *
    FROM shipment
    Where tracker_id = @tracker_id and is_active = 1 and is_deleted = 0
)
THEN CAST(1 AS BIT) 
ELSE CAST(0 AS BIT) 
END as 'is_used'