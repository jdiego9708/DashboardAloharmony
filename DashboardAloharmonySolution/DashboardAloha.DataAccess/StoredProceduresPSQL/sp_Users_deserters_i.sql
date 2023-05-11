CREATE OR REPLACE FUNCTION sp_Users_deserters_i(
OUT Id_users_deserter int,
IN Date_sync_users date,
IN Hour_sync_users time(2),
IN Count_users_active int,
IN Count_users_deserter int,
IN Count_users_deserter_free int,
IN Count_users_deserter_courtesy int,
IN Count_users_deserter_premium int,
IN Count_users_deserter_premium_plus int,
IN Percent_users_deserter_free decimal(19, 2),
IN Percent_users_deserter_courtesy decimal(19, 2),
IN Percent_users_deserter_premium decimal(19, 2),
IN Percent_users_deserter_premium_plus decimal(19, 2)
) AS $$
DECLARE
  Date_last_sync_users date;
BEGIN
  SELECT Date_sync_users INTO Date_last_sync_users FROM Total_sales ORDER BY Id_total_sale DESC LIMIT 1;

  IF (Date_sync_users > Date_last_sync_users) THEN
   INSERT INTO Users_deserters
		VALUES (@Date_sync_users, @Hour_sync_users, @Count_users_active, 
		@Count_users_deserter,
		@Count_users_deserter_free, @Count_users_deserter_courtesy, 
		@Count_users_deserter_premium, @Count_users_deserter_premium_plus, 
		@Percent_users_deserter_free, @Percent_users_deserter_courtesy, 
		@Percent_users_deserter_premium,@Percent_users_deserter_premium_plus)

  		RETURNING Id_users_deserter INTO Id_users_deserter;
  END IF;
END;
$$ LANGUAGE plpgsql;

