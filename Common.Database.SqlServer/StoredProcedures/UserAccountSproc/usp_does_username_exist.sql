CREATE PROCEDURE [dbo].[usp_does_username_exist]
    @username nvarchar(300),
	@exclude uniqueidentifier = NULL
    
AS
	Select id From user_account
	WHERE username = @username and is_deleted = 0   AND (@exclude  IS NULL OR (@exclude  IS NOT NULL AND id  !=  @exclude ))  
