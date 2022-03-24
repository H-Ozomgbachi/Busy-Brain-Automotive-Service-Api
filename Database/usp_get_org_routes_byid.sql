CREATE PROCEDURE [dbo].[usp_get_org_routes_byid]
    @org_id INT
AS
SELECT [id]
      ,[unique_id]
      ,[origin]
      ,[destination]      
  FROM [dbo].[route]
  Where [organisation_id] = @org_id and is_deleted = 0
  Order by [origin]