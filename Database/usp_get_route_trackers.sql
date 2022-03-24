CREATE PROCEDURE [dbo].[usp_get_route_trackers]
@route_id int
AS
SELECT [id]
      ,[unique_id]
      ,[name]
      ,[imie]
      ,[update_frequency]
      ,[is_active]
      ,[created_utc]
      ,[modified_utc]
      ,[organisation_id]
      ,route_id
  FROM [dbo].[tracker]
  WHERE route_id = @route_id