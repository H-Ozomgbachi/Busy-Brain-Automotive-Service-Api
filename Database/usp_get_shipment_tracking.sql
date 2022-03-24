CREATE PROCEDURE [dbo].[usp_get_shipment_tracking]
@shipment_id int
AS
WITH MaxTrackingTable AS
  (SELECT row_number() over(PARTITION BY [shipment_id],CONVERT(DATE, [created_utc])
                            ORDER BY [created_utc] desc) AS ID,
            st.[id] as tracking_id
           ,[shipment_id]
           ,[longitude]
           ,[latitude]
           ,[fromatted_address]
           ,[distance_to_destination_km]
           ,[meta_data]
           ,st.[created_utc]
   FROM [dbo].shipment_tracking st
   Where st.shipment_id = @shipment_id
   )
SELECT *
FROM MaxTrackingTable
WHERE MaxTrackingTable.ID = 1
  AND MaxTrackingTable.shipment_id = @shipment_id
  AND MaxTrackingTable.[created_utc] IS NOT NULL