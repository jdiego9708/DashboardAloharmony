using DasboardAloha.Entities.ModelsBinding;
using DashboardAloha.DataAccess.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace DashboardAloha.DataAccess.Dacs
{
    public class UsersDesertersDac : IUsersDesertersDac
    {
        #region CONSTRUCTOR
        private readonly IConnectionDac ConnectionDac;
        public UsersDesertersDac(IConnectionDac ConnectionDac)
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

        #region MÉTODO INSERT USER DESERTER
        public string InsertUserDeserter(UsersDeserterBindingModel user)
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
                    CommandText = "sp_Users_deserters_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_users_deserter = new()
                {
                    ParameterName = "@Id_users_deserter",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_users_deserter);

                SqlParameter Date_sync_users = new()
                {
                    ParameterName = "Date_sync_users",
                    SqlDbType = SqlDbType.Date,
                    Value = user.Date_sync_users,
                };
                SqlCmd.Parameters.Add(Date_sync_users);

                SqlParameter Hour_sync_users = new()
                {
                    ParameterName = "Hour_sync_users",
                    SqlDbType = SqlDbType.Time,
                    Value = user.Hour_sync_users,
                };
                SqlCmd.Parameters.Add(Hour_sync_users);

                SqlParameter Count_users_active = new()
                {
                    ParameterName = "Count_users_active",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_active,
                };
                SqlCmd.Parameters.Add(Count_users_active);

                SqlParameter Count_users_deserter = new()
                {
                    ParameterName = "Count_users_deserter",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_deserter,
                };
                SqlCmd.Parameters.Add(Count_users_deserter);

                SqlParameter Count_users_deserter_premium = new()
                {
                    ParameterName = "Count_users_deserter_premium",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_deserter_premium,
                };
                SqlCmd.Parameters.Add(Count_users_deserter_premium);

                SqlParameter Count_users_deserter_premium_plus = new()
                {
                    ParameterName = "Count_users_deserter_premium_plus",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_deserter_premium_plus,
                };
                SqlCmd.Parameters.Add(Count_users_deserter_premium_plus);

                SqlParameter Percent_users_deserter_premium = new()
                {
                    ParameterName = "Percent_users_deserter_premium",
                    SqlDbType = SqlDbType.Decimal,
                    Value = user.Percent_users_deserter_premium,
                };
                SqlCmd.Parameters.Add(Percent_users_deserter_premium);

                SqlParameter Percent_users_deserter_premium_plus = new()
                {
                    ParameterName = "Percent_users_deserter_premium_plus",
                    SqlDbType = SqlDbType.Decimal,
                    Value = user.Percent_users_deserter_premium_plus,
                };
                SqlCmd.Parameters.Add(Percent_users_deserter_premium_plus);
               
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;

                user.Id_users_deserter = Convert.ToInt32(SqlCmd.Parameters["@Id_users_deserter"].Value);
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
