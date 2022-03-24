CREATE PROCEDURE [dbo].[usp_shipper_event_add]
	 @event_type nvarchar(max)
    ,@created datetime2(7)
    ,@event_body nvarchar(max)
    ,@shipper_id int
    ,@changed_by_user_id uniqueidentifier 

    AS
INSERT INTO [dbo].[shipper_events]
           ([event_type]
           ,[created]
           ,[event_body]
           ,[shipper_id]
           ,[changed_by_user_id])
     VALUES
           (@event_type 
            ,@created 
            ,@event_body 
            ,@shipper_id 
            ,@changed_by_user_id)