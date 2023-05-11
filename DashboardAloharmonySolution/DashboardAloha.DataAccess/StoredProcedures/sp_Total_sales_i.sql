CREATE OR ALTER PROC sp_Total_sales_i
@Id_total_sale int output,
@Date_sync_users date,
@Hour_sync_users time(2),
@Count_users_active int,
@Count_users_payments int,
@Count_users_sales int,
@Count_users_sales_premium int,
@Count_users_sales_premium_plus int,
@Percent_users_sales_premium decimal(19, 2),
@Percent_users_sales_premium_plus decimal(19, 2)
AS
BEGIN
	DECLARE @Date_last_sync_users date = (SELECT TOP 1 Date_sync_users
	FROM Total_sales ORDER BY Id_total_sale DESC)

	IF (@Date_sync_users > @Date_last_sync_users)
	BEGIN

		INSERT INTO Total_sales
		VALUES (@Date_sync_users, @Hour_sync_users, @Count_users_active, @Count_users_payments,
		@Count_users_sales, @Count_users_sales_premium, @Count_users_sales_premium_plus,
		@Percent_users_sales_premium, @Percent_users_sales_premium_plus)

		SET @Id_total_sale = SCOPE_IDENTITY();

	END
END