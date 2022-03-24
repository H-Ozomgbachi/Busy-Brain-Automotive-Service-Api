

CREATE PROCEDURE [dbo].[usp_User_Update]
    @record_Id INT,
	@firstname NVARCHAR(256), 
    @lastname NVARCHAR(256),     
    @email NVARCHAR(1024),    
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
    @is_deleted BIT,
    @organisation_id Int = NULL
    
AS
	UPDATE user_account 
    SET

    firstname =@firstname,
    lastname=@lastname,
    email = @email,
    [version] = @version, 
    [status] = @status, 
    password_hash = @password_hash, 
    password_salt = @password_salt, 
    phone = @phone, 
    country_code = @country_code, 
    roles = @roles, 
    password_reset_token = @password_reset_token, 
    last_modified_utc = @last_modified_utc, 
    last_login_utc = @last_login_utc, 
    force_password_reset = @force_password_reset,
    is_deleted = @is_deleted,
    organisation_id = @organisation_id

	WHERE record_Id = @record_Id