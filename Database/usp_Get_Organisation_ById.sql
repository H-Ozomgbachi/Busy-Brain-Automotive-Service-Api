CREATE PROCEDURE [dbo].[usp_Get_Organisation_ById]
    @org_id int	
    
AS
	Select * From organisation
	WHERE id = @org_id