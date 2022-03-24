CREATE PROCEDURE [dbo].[usp_add_location_tracking_event] 
	-- Add the parameters for the stored procedure here
	@event NVARCHAR(50), 
	@event_data NVARCHAR(MAX),
	@imie NVARCHAR(MAX)
AS
BEGIN


INSERT INTO [dbo].[tracking_device_events]
           ([event]
           ,[event_data]
           ,[imie])
     VALUES
           (@event
           ,@event_data
           ,@imie)
END