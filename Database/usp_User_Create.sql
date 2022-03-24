CREATE PROCEDURE [dbo].[usp_User_Create]
	@firstname NVARCHAR(256), 
    @lastname NVARCHAR(256),     
    @email NVARCHAR(1024), 
    @username NVARCHAR(1024),
    @password_hash varbinary(max),
    @password_salt varbinary(max),
    @phone NVARCHAR(50),
    @country_code NVARCHAR(10),
    @roles NVARCHAR(Max),
    @version INT, 
    @status  INT, 
    @password_reset_token UNIQUEIDENTIFIER,     
    @last_modified_utc DATETIME2, 
    @last_login_utc DATETIME2,
    @force_password_reset BIT,
	@org_id INT
    
AS
	INSERT INTO user_account (firstname, lastname, email, username, version, status, password_hash, password_salt, phone, country_code, roles, password_reset_token, last_modified_utc, last_login_utc, force_password_reset, organisation_id)
	OUTPUT inserted.record_Id
	VALUES (@firstname, @lastname, @email, @username, @version,@status ,@password_hash, @password_salt,@phone, @country_code, @roles,@password_reset_token, @last_modified_utc,@last_login_utc,@force_password_reset, @org_id);