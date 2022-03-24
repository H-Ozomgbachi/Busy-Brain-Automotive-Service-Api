CREATE PROCEDURE [dbo].[usp_shipment_update]
	 @id  int, 
     @recipient_name nvarchar(300) ,
     @recipient_address  nvarchar(300) ,
     @recipient_phone  nvarchar(300) ,
     @price  decimal(10,2) ,
     @weight  decimal(10,2) ,
     @delivery_date_utc  datetime2(7) ,
     @estimated_delivery_date_utc  datetime2(7),
     @is_deleted  bit ,
     @is_active  bit ,
     @shipper_id  int ,
     @version  int ,
     @driver_phone  nvarchar(50) ,
     @driver_name  nvarchar(50),
     @tracker_id  int
    
AS
UPDATE [dbo].[shipment]
   SET 
       [recipient_name] = @recipient_name
      ,[recipient_address] = @recipient_address
      ,[recipient_phone] = @recipient_phone
      ,[price] = @price
      ,[weight] = @weight
      ,[delivery_date_utc] = @delivery_date_utc
      ,[estimated_delivery_date_utc] = @estimated_delivery_date_utc
      ,[last_modified_utc] = GETUTCDATE()      
      ,[is_deleted] = @is_deleted
      ,[is_active] = @is_active
      ,[shipper_id] = @shipper_id
      ,[version] = @version
      ,[driver_phone] = @driver_phone
      ,[driver_name] = @driver_name
      ,[tracker_id] = @tracker_id
 WHERE id =@id