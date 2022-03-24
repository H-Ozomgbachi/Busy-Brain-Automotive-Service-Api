CREATE PROCEDURE [dbo].[usp_get_users]

AS
	BEGIN
	SELECT id, firstname, lastname, email, phone, roles, position_in_organisation FROM user_account
END
