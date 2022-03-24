CREATE PROCEDURE [dbo].[usp_get_shipment_byid]
	 @id  int,
     @org_id  int
AS
SELECT [id]
      ,[unique_id]
      ,[recipient_name]
      ,[recipient_address]
      ,[recipient_phone]
      ,[price]
      ,[weight]
      ,[delivery_date_utc]
      ,[estimated_delivery_date_utc]
      ,[created_utc]
      ,[last_modified_utc]
      ,[organisation_id]
      ,[tracking_number]
      ,[is_deleted]
      ,[is_active]
      ,[route_id]
      ,[shipper_id]
      ,[version]
      ,[driver_phone]
      ,[driver_name]
      ,[tracker_id]
  FROM [dbo].[shipment]

  WHERE id=@id and organisation_id = @org_id and is_deleted = 0