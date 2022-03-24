CREATE PROCEDURE [dbo].[usp_shipment_add]
	
     @recipient_name nvarchar(300) ,
     @recipient_address  nvarchar(300) ,
     @recipient_phone  nvarchar(300) ,
     @price  decimal(10,2) ,
     @weight  decimal(10,2) ,
     @delivery_date_utc  datetime2(7) ,
     @estimated_delivery_date_utc  datetime2(7),
     @organisation_id  int ,
     @tracking_number  nvarchar(300) ,
     @is_deleted  bit ,
     @is_active  bit ,
     @route_id  int ,
     @shipper_id  int ,
     @version  int ,
     @driver_phone  nvarchar(50) ,
     @driver_name  nvarchar(50),
     @tracker_id int
AS
INSERT INTO [dbo].[shipment]
           ([unique_id]
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
           ,[tracker_id]
           ,[shipper_id]
           ,[version]
           ,[driver_phone]
           ,[driver_name])
    OUTPUT inserted.id
	     VALUES
           ( NEWID()
           ,@recipient_name
           ,@recipient_address
           ,@recipient_phone
           ,@price
           ,@weight
           ,@delivery_date_utc
           ,@estimated_delivery_date_utc
           ,GETUTCDATE()
           ,GETUTCDATE()
           ,@organisation_id
           ,@tracking_number
           ,@is_deleted
           ,@is_active
           ,@route_id
           ,@tracker_id
           ,@shipper_id
           ,@version
           ,@driver_phone
           ,@driver_name);