CREATE PROCEDURE [dbo].[usp_shipment_delay_upsert]
@shipment_id int,
@fromatted_address NVARCHAR(2500),
@longitude decimal(9,6),
@latitude decimal(9,6),
@duration decimal(10,2),
@meta_data nvarchar(max)
AS

DECLARE @orig geography = geography::Point(@latitude, @longitude, 4326);
DECLARE @ItemID int;

SELECT top 1 dest.id 
INTO #includeDistances
FROM [dbo].[shipment_delay] dest
WHERE shipment_id = @shipment_id 
AND @orig.STDistance(geography::Point(dest.latitude, dest.longitude, 4326)) <= 100 -- distance less than 100 meters
AND DATEDIFF(MINUTE, dest.created_utc , GETUTCDATE()) <= 10 -- time difference is less than 10mins
order by id desc;



IF EXISTS (select * from #includeDistances)
    BEGIN
        select top 1 @ItemID = id  from #includeDistances  order by Id asc;

        UPDATE [dbo].[shipment_delay]
           SET [shipment_id] = @shipment_id
              ,[longitude] = @longitude
              ,[latitude] = @latitude
              ,[fromatted_address] = @fromatted_address
              ,[duration] = [duration] + @duration
              ,[meta_data] = @meta_data
              --,created_utc =  GETUTCDATE()
        WHERE id = @ItemID  AND shipment_id = @shipment_id;
                
    END
ELSE
    BEGIN
    INSERT INTO [dbo].[shipment_delay]
           ([shipment_id]
           ,[longitude]
           ,[latitude]
           ,[fromatted_address]
           ,[duration]
           ,[meta_data]
           ,[created_utc])
     VALUES
           (@shipment_id
           ,@longitude
           ,@latitude
           ,@fromatted_address
           ,@duration
           ,@meta_data
           ,GETUTCDATE())
    END

