CREATE PROCEDURE [dbo].[usp_get_users_in_organisation]
	@orgId int
AS
	SELECT id, firstname, lastname, email, phone FROM user_account
	WHERE organisation_id = @orgId
