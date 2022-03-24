CREATE PROCEDURE [dbo].[usp_shipment_event_add]
	 @event_type nvarchar(max)
    ,@created datetime2(7)
    ,@event_body nvarchar(max)
    ,@shipment_id int
    ,@changed_by_user_id uniqueidentifier 

    AS
INSERT INTO [dbo].[shipment_events]
           ([event_type]
           ,[created]
           ,[event_body]
           ,[shipment_id]
           ,[changed_by_user_id])
     VALUES
           (@event_type 
            ,@created 
            ,@event_body 
            ,@shipment_id 
            ,@changed_by_user_id)