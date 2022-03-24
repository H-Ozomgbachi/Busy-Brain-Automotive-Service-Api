CREATE PROCEDURE [dbo].[usp_get_route_byid]
    @id INT,
    @org_id INT
AS
SELECT [id]
      ,[unique_id]
      ,[origin]
      ,[destination]
      ,[created_utc]
      ,[organisation_id]
      ,[version]
      ,[is_deleted]
  FROM [dbo].[route]
  Where id = @id and is_deleted = 0 and [organisation_id] = @org_id