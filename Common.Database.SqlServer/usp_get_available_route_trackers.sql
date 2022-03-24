CREATE PROCEDURE [dbo].[usp_get_available_route_trackers]
@route_id int,
@org_id int
AS
SELECT [id]
      ,[name]
  FROM [dbo].[tracker] t  
  WHERE t.route_id = @route_id 
  and t.organisation_id = @org_id
  and  t.id NOT IN (Select s.tracker_id 
                    From [dbo].[shipment] s 
                    Where s.route_id =@route_id 
                    and s.organisation_id = @org_id
                    and s.is_active = 1 
                    and s.is_deleted = 0)