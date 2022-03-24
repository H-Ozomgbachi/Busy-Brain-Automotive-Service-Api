CREATE PROCEDURE [dbo].[usp_get_shipment_delays]
@shipment_id int,
@org_id int
AS

SELECT d.id,d.duration,d.fromatted_address,d.latitude, d.longitude,d.meta_data, d.created_utc
FROM [dbo].[shipment_delay] d
JOIN dbo.shipment s on s.id = d.shipment_id
WHERE shipment_id = @shipment_id  AND s.id = @shipment_id  AND s.organisation_id = @org_id
order by d.id desc;
