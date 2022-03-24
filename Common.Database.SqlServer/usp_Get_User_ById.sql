CREATE PROCEDURE [dbo].[usp_Get_User_ById]
    @user_id int	
    
AS
	Select * From user_account
	WHERE record_Id = @user_id and is_deleted = 0