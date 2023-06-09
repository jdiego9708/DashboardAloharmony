CREATE OR ALTER PROC sp_Users_registers_i
@Id_users_registers int output,
@Date_sync_users date,
@Hour_sync_users time(2),
@Count_users_registers int,
@Count_users_registers_free int,
@Count_users_registers_courtesy int,
@Count_users_registers_premium int,
@Count_users_registers_premium_plus int,
@Count_users_registers_active int,
@Percent_users_registers_active decimal(19, 2)
AS
BEGIN
	DECLARE @Date_last_sync_users date = (SELECT TOP 1 Date_sync_users
	FROM Total_sales ORDER BY Id_total_sale DESC)

	IF (@Date_sync_users > @Date_last_sync_users)
	BEGIN
	
		INSERT INTO Users_registers
		VALUES (@Date_sync_users, @Hour_sync_users, @Count_users_registers,
		@Count_users_registers_free, @Count_users_registers_courtesy, 
		@Count_users_registers_premium, @Count_users_registers_premium_plus, 
		@Count_users_registers_active, @Percent_users_registers_active)
	
		SET @Id_users_registers = SCOPE_IDENTITY();
	END
END