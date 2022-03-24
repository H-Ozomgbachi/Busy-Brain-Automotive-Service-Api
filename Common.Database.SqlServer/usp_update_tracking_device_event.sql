CREATE PROCEDURE [dbo].[usp_update_tracking_device_event] 
	-- Add the parameters for the stored procedure here	
	@status int,
	@id int
AS
Update [dbo].[tracking_device_events]
SET [status] =@status
	WHERE id = @id 
