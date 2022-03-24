CREATE PROCEDURE [dbo].[usp_shipment_tracking_upsert]
@id INT,
@shipment_id int,
@fromatted_address NVARCHAR(2500),
@longitude decimal(9,6),
@latitude decimal(9,6),
@distance_to_destination_km decimal(10,2),
@meta_data nvarchar(max)
AS
IF EXISTS (SELECT 1 FROM [dbo].[shipment_tracking] WHERE id = @id AND shipment_id = @shipment_id)
    BEGIN
        UPDATE [dbo].[shipment_tracking]
           SET [shipment_id] = @shipment_id
              ,[longitude] = @longitude
              ,[latitude] = @latitude
              ,[fromatted_address] = @fromatted_address
              ,[distance_to_destination_km] = @distance_to_destination_km
              ,[meta_data] = @meta_data
        WHERE id = @id
                
    END
ELSE
    BEGIN
    INSERT INTO [dbo].[shipment_tracking]
           ([shipment_id]
           ,[longitude]
           ,[latitude]
           ,[fromatted_address]
           ,[distance_to_destination_km]
           ,[meta_data]
           ,[created_utc])
     VALUES
           (@shipment_id
           ,@longitude
           ,@latitude
           ,@fromatted_address
           ,@distance_to_destination_km
           ,@meta_data
           ,GETUTCDATE())
    END

