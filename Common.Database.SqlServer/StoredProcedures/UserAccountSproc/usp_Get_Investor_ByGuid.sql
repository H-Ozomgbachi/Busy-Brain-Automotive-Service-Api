CREATE PROCEDURE [dbo].[usp_Get_Investor_ByGuid]
	@investorGuid uniqueidentifier
AS
	SELECT * FROM investor
		WHERE current_user_id = @investorGuid

