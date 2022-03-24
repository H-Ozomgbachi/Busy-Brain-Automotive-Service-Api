CREATE PROCEDURE [dbo].[usp_shipper_add]
	@name nvarchar(256),
    @contact_email nvarchar(300),
    @phone nvarchar(50),
    @report_email nvarchar(300)
AS
INSERT INTO [dbo].[shipper]
           ([unique_id]
           ,[name]
           ,[contact_email]
           ,[phone]
           ,[report_email]
           ,[created_utc]
           ,[last_modified_utc])
	 OUTPUT inserted.id
     VALUES
           (NEWID()
           ,@name
           ,@contact_email
           ,@phone
           ,@report_email
           ,GETUTCDATE()
           ,GETUTCDATE())