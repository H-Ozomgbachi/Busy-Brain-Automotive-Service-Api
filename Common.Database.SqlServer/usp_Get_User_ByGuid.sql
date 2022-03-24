

CREATE PROCEDURE [dbo].[usp_Get_User_ByGuid]
    @user_Id UniqueIdentifier	
    
AS
	Select * From user_account
	WHERE id = @user_Id and is_deleted = 0