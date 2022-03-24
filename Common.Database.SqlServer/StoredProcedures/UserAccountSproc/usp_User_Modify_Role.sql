CREATE PROCEDURE [dbo].[usp_User_Modify_Role]
    @userId UNIQUEIDENTIFIER,
    @roles NVARCHAR(Max)
    
AS
	UPDATE user_account 
    SET
    roles = @roles 

	WHERE id = @userId
