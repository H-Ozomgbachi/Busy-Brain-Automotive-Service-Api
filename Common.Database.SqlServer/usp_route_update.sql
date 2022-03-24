CREATE PROCEDURE [dbo].[usp_route_update]
    @id int,
	@origin nvarchar(100),
    @destination nvarchar(100),
    @organisation_id INT,
    @version INT,
    @is_deleted BIT
AS
UPDATE [dbo].[route]
   SET [origin] = @origin
      ,[destination] = @destination
      ,[version] =@version
      ,[is_deleted] = @is_deleted
 WHERE id =@id