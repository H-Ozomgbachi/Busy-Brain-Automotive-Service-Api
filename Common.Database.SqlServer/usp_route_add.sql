CREATE PROCEDURE [dbo].[usp_route_add]
	@origin nvarchar(100),
    @destination nvarchar(100),
    @organisation_id INT,
    @version INT
AS
INSERT INTO [dbo].[route]
           ([unique_id]
           ,[origin]
           ,[destination]
           ,[created_utc]
           ,[organisation_id]
           ,[version])
     VALUES
           (NEWID()
           ,@origin
           ,@destination
           ,GETUTCDATE()
           ,@organisation_id
           ,@version)