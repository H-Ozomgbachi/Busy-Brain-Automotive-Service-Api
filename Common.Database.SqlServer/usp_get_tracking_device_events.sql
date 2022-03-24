CREATE PROCEDURE [dbo].[usp_get_tracking_device_events] 
	-- Add the parameters for the stored procedure here
	@event NVARCHAR(50), 
	@status int,
	@size int,
	@from DateTime,
	@to DateTime
AS
SELECT id,event_data
	FROM [dbo].[tracking_device_events]
	WHERE [status] = @status and [event] = @event and created_utc between @from and @to
	ORDER BY [id]  
	OFFSET @size * (1 - 1) ROWS  
	FETCH NEXT @size ROWS ONLY
