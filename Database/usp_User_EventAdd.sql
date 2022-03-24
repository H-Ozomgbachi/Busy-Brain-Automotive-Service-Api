CREATE PROCEDURE [dbo].[usp_User_EventAdd]
	@event_type NVARCHAR(MAX),
    @event_body NVARCHAR(MAX),
    @created DATETIME2,
    @user_id uniqueidentifier,
    @changed_by_user_id uniqueidentifier
AS
INSERT INTO user_account_event
(event_type, event_body, created, changed_by_user_id, user_id)
VALUES
(@event_type, @event_body, @created, @changed_by_user_id, @user_id)