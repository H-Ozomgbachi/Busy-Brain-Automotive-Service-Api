CREATE PROCEDURE [dbo].[usp_organisation_event_add]
	 @event_type nvarchar(max)
    ,@created datetime2(7)
    ,@event_body nvarchar(max)
    ,@org_id int
    ,@changed_by_user_id uniqueidentifier 
	,@account_type nvarchar(max)

    AS
INSERT INTO [dbo].[organisation_events]
           ([event_type]
           ,[created]
           ,[event_body]
           ,[org_id]
           ,[changed_by_user_id]
		   ,[account_type])
     VALUES
           (@event_type 
            ,@created 
            ,@event_body 
            ,@org_id 
            ,@changed_by_user_id
			,@account_type)