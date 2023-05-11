CREATE OR ALTER PROC sp_Users_deserters_i
@Id_users_deserter int output,
@Date_sync_users date,
@Hour_sync_users time(2),
@Count_users_active int,
@Count_users_deserter int,
@Count_users_deserter_free int,
@Count_users_deserter_courtesy int,
@Count_users_deserter_premium int,
@Count_users_deserter_premium_plus int,
@Percent_users_deserter_free decimal(19, 2),
@Percent_users_deserter_courtesy decimal(19, 2),
@Percent_users_deserter_premium decimal(19, 2),
@Percent_users_deserter_premium_plus decimal(19, 2)
AS
BEGIN
	DECLARE @Date_last_sync_users date = (SELECT TOP 1 Date_sync_users
	FROM Users_deserters ORDER BY Id_users_deserter DESC)

	IF (@Date_sync_users > @Date_last_sync_users)
	BEGIN
		INSERT INTO Users_deserters
		VALUES (@Date_sync_users, @Hour_sync_users, @Count_users_active, 
		@Count_users_deserter,
		@Count_users_deserter_free, @Count_users_deserter_courtesy, 
		@Count_users_deserter_premium, @Count_users_deserter_premium_plus, 
		@Percent_users_deserter_free, @Percent_users_deserter_courtesy, 
		@Percent_users_deserter_premium,@Percent_users_deserter_premium_plus)
		SET @Id_users_deserter = SCOPE_IDENTITY();
	END	
END