CREATE PROCEDURE [dbo].[usp_Get_Shipper_ById]
    @shipper_id int	
    
AS
	Select * From shipper
	WHERE id = @shipper_id