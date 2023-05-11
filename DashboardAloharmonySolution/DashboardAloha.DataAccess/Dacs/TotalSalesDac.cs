using DasboardAloha.Entities.ModelsBinding;
using DashboardAloha.DataAccess.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace DashboardAloha.DataAccess.Dacs
{
    public class TotalSalesDac : ITotalSalesDac
    {
        #region CONSTRUCTOR
        private readonly IConnectionDac ConnectionDac;
        public TotalSalesDac(IConnectionDac ConnectionDac)
        {
            this.ConnectionDac = ConnectionDac;

            this.Mensaje_error = string.Empty;
        }
        #endregion

        #region MENSAJE
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            string mensaje_error = e.Message;
            if (e.Errors != null)
            {
                if (e.Errors.Count > 0)
                {
                    mensaje_error += string.Join("|", e.Errors);
                }
            }
            this.Mensaje_error = mensaje_error;
        }
        #endregion

        #region PROPIEDADES
        public string Mensaje_error { get; set; }
        #endregion

        #region MÉTODO INSERT TOTAL SALES
        public string InsertTotalSale(TotalSalesBindingModel total)
        {
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                SqlCon.ConnectionString = ConnectionDac.CnSQL();
                SqlCon.Open();

                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Total_sales_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_total_sale = new()
                {
                    ParameterName = "@Id_total_sale",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_total_sale);

                SqlParameter Date_sync_users = new()
                {
                    ParameterName = "Date_sync_users",
                    SqlDbType = SqlDbType.Date,
                    Value = total.Date_sync_users,
                };
                SqlCmd.Parameters.Add(Date_sync_users);

                SqlParameter Hour_sync_users = new()
                {
                    ParameterName = "Hour_sync_users",
                    SqlDbType = SqlDbType.Time,
                    Value = total.Hour_sync_users,
                };
                SqlCmd.Parameters.Add(Hour_sync_users);

                SqlParameter Count_users_active = new()
                {
                    ParameterName = "Count_users_active",
                    SqlDbType = SqlDbType.Int,
                    Value = total.Count_users_active,
                };
                SqlCmd.Parameters.Add(Count_users_active);

                SqlParameter Count_users_payments = new()
                {
                    ParameterName = "Count_users_payments",
                    SqlDbType = SqlDbType.Int,
                    Value = total.Count_users_payments,
                };
                SqlCmd.Parameters.Add(Count_users_payments);

                SqlParameter Count_users_sales = new()
                {
                    ParameterName = "Count_users_sales",
                    SqlDbType = SqlDbType.Int,
                    Value = total.Count_users_sales,
                };
                SqlCmd.Parameters.Add(Count_users_sales);

                SqlParameter Count_users_sales_premium = new()
                {
                    ParameterName = "Count_users_sales_premium",
                    SqlDbType = SqlDbType.Int,
                    Value = total.Count_users_sales_premium,
                };
                SqlCmd.Parameters.Add(Count_users_sales_premium);

                SqlParameter Count_users_sales_premium_plus = new()
                {
                    ParameterName = "Count_users_sales_premium_plus",
                    SqlDbType = SqlDbType.Int,
                    Value = total.Count_users_sales_premium_plus,
                };
                SqlCmd.Parameters.Add(Count_users_sales_premium_plus);

                SqlParameter Percent_users_sales_premium = new()
                {
                    ParameterName = "Percent_users_sales_premium",
                    SqlDbType = SqlDbType.Decimal,
                    Value = total.Percent_users_sales_premium,
                };
                SqlCmd.Parameters.Add(Percent_users_sales_premium);

                SqlParameter Percent_users_sales_premium_plus = new()
                {
                    ParameterName = "Percent_users_sales_premium_plus",
                    SqlDbType = SqlDbType.Decimal,
                    Value = total.Percent_users_sales_premium_plus,
                };
                SqlCmd.Parameters.Add(Percent_users_sales_premium_plus);
              
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;

                total.Id_total_sale = Convert.ToInt32(SqlCmd.Parameters["@Id_total_sale"].Value);
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion
    }
}
