CREATE PROCEDURE [dbo].[usp_Get_User_ByUsername]
    @username nvarchar(300)	
    
AS
	Select * From user_account
	WHERE username = @username and is_deleted = 0
