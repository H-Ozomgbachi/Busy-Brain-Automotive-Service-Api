CREATE PROCEDURE [dbo].[usp_count_users_in_organisation]
	@org_id int
AS
BEGIN
	SELECT COUNT(*) AS users_in_organisation FROM user_account
	WHERE organisation_id = @org_id
END
