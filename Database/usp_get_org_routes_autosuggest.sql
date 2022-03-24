CREATE PROCEDURE [dbo].[usp_get_org_routes_autosuggest]
    @org_id INT,
    @query NVARCHAR(300)
AS
SELECT [id]
      ,[unique_id]
      ,[origin]
      ,[destination]
  FROM [dbo].[route]
  Where [origin] LIKE '%' + @query + '%'  AND  [organisation_id] = @org_id AND is_deleted = 0
  UNION 
  SELECT [id]
      ,[unique_id]
      ,[origin]
      ,[destination]
  FROM [dbo].[route]
  Where [destination] LIKE '%' + @query + '%'  AND  [organisation_id] = @org_id AND is_deleted = 0
  ORDER BY origin;