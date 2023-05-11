CREATE OR ALTER PROC sp_Users_i
	@Id_user int output,
	@Date_sync_users date,
	@Hour_sync_users time(2),
	@Created_user date,
	@Email_user varchar (100),
	@Phone varchar(50),
	@Device varchar(50),
	@Full_name varchar(200),
	@Last_name varchar(200),
	@Profile varchar(50),
	@Gender varchar(50),
	@Membership varchar(50),
	@Country varchar(50),
	@Status_membership varchar(50),
	@Total_time_elapsed decimal(19, 2)
AS
BEGIN

	--DECLARE @Date_last_sync_users date = (SELECT TOP 1 Date_sync_users
	--FROM Users ORDER BY Id_user DESC)

	--IF (@Date_sync_users > @Date_last_sync_users)
	--BEGIN	

		INSERT INTO Users
		VALUES (@Date_sync_users, @Hour_sync_users, @Created_user, @Email_user, @Phone, @Device, @Full_name, @Last_name, 
		@Profile, @Gender, @Membership, @Country, @Status_membership, @Total_time_elapsed)

		SET @Id_user = SCOPE_IDENTITY();

	--END
END


