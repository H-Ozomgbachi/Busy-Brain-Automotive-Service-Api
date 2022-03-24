CREATE PROCEDURE [dbo].[usp_get_shipment_by_tracker] 
	-- Add the parameters for the stored procedure here
	@imie NVARCHAR(MAX)
AS
SELECT Top 1 t.[id] as tracker_id
      ,s.[organisation_id]
	  ,s.id as shipment_id
	  ,r.destination
	  ,r.origin
  FROM [dbo].tracker t
  JOIN [dbo].shipment s ON s.tracker_id = t.id
  JOIN [dbo].[route] r on s.route_id = r.id
  Where imie = @imie and s.is_active = 1