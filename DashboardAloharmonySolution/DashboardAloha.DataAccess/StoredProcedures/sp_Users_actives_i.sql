CREATE OR ALTER PROC sp_Users_actives_i
@Id_users_actives int output,
@Date_sync_users date,
@Hour_sync_users time(2),
@Count_users_registers int,
@Count_users_active int,
@Percent_users_active decimal(19, 2),
@Count_users_active_free int,
@Count_users_active_courtesy int,
@Count_users_active_premium int,
@Count_users_active_premium_plus int,
@Percent_users_active_free decimal(19, 2),
@Percent_users_active_courtesy decimal(19, 2),
@Percent_users_active_premium decimal(19, 2),
@Percent_users_active_premium_plus decimal(19, 2)
AS
BEGIN
	DECLARE @Date_last_sync_users date = (SELECT TOP 1 Date_sync_users
	FROM Users_actives ORDER BY Id_users_actives DESC)

	IF (@Date_sync_users > @Date_last_sync_users)
	BEGIN
		INSERT INTO Users_actives
		VALUES (@Date_sync_users, @Hour_sync_users, @Count_users_registers,
		@Count_users_active, @Percent_users_active, 
		@Count_users_active_free, @Count_users_active_courtesy, 
		@Count_users_active_premium, @Count_users_active_premium_plus, 
		@Percent_users_active_free, @Percent_users_active_courtesy, 
		@Percent_users_active_premium, @Percent_users_active_premium_plus)
		SET @Id_users_actives = SCOPE_IDENTITY();
	END

END