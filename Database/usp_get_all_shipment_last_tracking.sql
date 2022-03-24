CREATE PROCEDURE [dbo].[usp_get_all_shipment_last_tracking]
@org_id int
AS
WITH MaxTrackingTable AS
  (SELECT row_number() over(PARTITION BY [shipment_id]
                            ORDER BY st.[id] desc) AS ID,
            st.[id] as tracking_id
           ,[shipment_id]
           ,[longitude]
           ,[latitude]
           ,[fromatted_address]
           ,[distance_to_destination_km]
           ,[meta_data]
           ,st.[created_utc]
           ,s.organisation_id
   FROM [dbo].shipment_tracking st
   JOIN shipment s on s.id = st.shipment_id
   Where s.organisation_id = @org_id and s.is_deleted = 0 and s.is_active = 1
   )
SELECT *
FROM MaxTrackingTable
WHERE MaxTrackingTable.ID = 1
  AND MaxTrackingTable.organisation_id = @org_id
  AND MaxTrackingTable.[created_utc] IS NOT NULL