CREATE PROCEDURE [dbo].[usp_route_event_add]
	 @event_type nvarchar(max)
    ,@created datetime2(7)
    ,@event_body nvarchar(max)
    ,@route_id int
    ,@changed_by_user_id uniqueidentifier 

    AS
INSERT INTO [dbo].[route_events]
           ([event_type]
           ,[created]
           ,[event_body]
           ,[route_id]
           ,[changed_by_user_id])
     VALUES
           (@event_type 
            ,@created 
            ,@event_body 
            ,@route_id 
            ,@changed_by_user_id)