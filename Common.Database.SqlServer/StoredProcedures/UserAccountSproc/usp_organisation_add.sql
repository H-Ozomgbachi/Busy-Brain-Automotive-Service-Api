CREATE PROCEDURE [dbo].[usp_organisation_add]
	@name NVARCHAR(256),
    @contact_email NVARCHAR(300),
    @phone NVARCHAR(50),
    @report_email nvarchar(300),
	@account_type nvarchar(300)
AS
INSERT INTO [dbo].[organisation]
           ([unique_id]
           ,[name]
           ,[contact_email]
           ,[phone]
           ,[report_email]
           ,[created_utc]
           ,[last_modified_utc]
		   ,[account_type])
    OUTPUT inserted.id
     VALUES
           (NEWID()
           ,@name
           ,@contact_email
           ,@phone
           ,@report_email
           ,GETUTCDATE()
           ,GETUTCDATE()
		   ,@account_type)