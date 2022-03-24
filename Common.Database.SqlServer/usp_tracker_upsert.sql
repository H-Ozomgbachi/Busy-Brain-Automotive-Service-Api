CREATE PROCEDURE [dbo].[usp_tracker_upsert]
@id INT,
@name NVARCHAR(256),
@imie nvarchar(100),
@update_frequency int,
@is_active bit,
@organisation_id int,
@route_id int
AS
IF EXISTS (SELECT 1 FROM [dbo].[tracker] WHERE id = @id)
    BEGIN
        UPDATE [dbo].[tracker]
           SET [name] = @name
              ,[imie] = @imie
              ,[update_frequency] = @update_frequency
              ,[is_active] = @is_active               
              ,[modified_utc] = GETUTCDATE()
              ,[organisation_id] = @organisation_id
              ,route_id = @route_id
        WHERE id = @id
                
    END
ELSE
    BEGIN
INSERT INTO [dbo].[tracker]
           ([unique_id]
           ,[name]
           ,[imie]
           ,[update_frequency]
           ,[is_active]
           ,[created_utc]
           ,[modified_utc]
           ,[organisation_id]           
           ,[route_id])
     VALUES
           (NEWID()
           ,@name
           ,@imie
           ,@update_frequency
           ,@is_active
           ,GETUTCDATE()
           ,GETUTCDATE()
           ,@organisation_id           
           ,@route_id)
     END