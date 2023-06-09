CREATE OR ALTER PROC sp_Users_genders_i
@Id_users_gender int output,
@Date_sync_users date,
@Hour_sync_users time(2),
@Count_users_registers int,
@Count_users_registers_masculine int,
@Count_users_registers_female int,
@Percent_users_registers_female decimal(19, 2),
@Percent_users_registers_masculine decimal(19, 2),
@Count_users_actives int,
@Count_users_actives_masculine int,
@Count_users_actives_female int,
@Percent_users_actives_masculine decimal(19, 2),
@Percent_users_actives_female decimal(19, 2)
AS
BEGIN
	
	DECLARE @Date_last_sync_users date = (SELECT TOP 1 Date_sync_users
	FROM Users_genders ORDER BY Id_users_gender DESC)

	IF (@Date_sync_users > @Date_last_sync_users)
	BEGIN
		INSERT INTO Users_genders
		VALUES (@Date_sync_users, @Hour_sync_users, @Count_users_registers, 
		@Count_users_registers_masculine, @Count_users_registers_female, 
		@Percent_users_registers_female, @Percent_users_registers_masculine, 
		@Count_users_actives, @Count_users_actives_masculine, @Count_users_actives_female,
		@Percent_users_actives_masculine, @Percent_users_actives_female)	
		SET @Id_users_gender = SCOPE_IDENTITY();
	END
END