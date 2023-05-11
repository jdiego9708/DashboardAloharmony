using DasboardAloha.Entities.ModelsBinding;
using DashboardAloha.DataAccess.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace DashboardAloha.DataAccess.Dacs
{
    public class UsersGendersDac : IUsersGendersDac
    {
        #region CONSTRUCTOR
        private readonly IConnectionDac ConnectionDac;
        public UsersGendersDac(IConnectionDac ConnectionDac)
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

        #region MÉTODO INSERT USER GENDER
        public string InsertUserGender(UsersGendersBindingModel user)
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
                    CommandText = "sp_Users_genders_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_users_gender = new()
                {
                    ParameterName = "@Id_users_gender",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_users_gender);

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

                SqlParameter Count_users_registers = new()
                {
                    ParameterName = "Count_users_registers",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_registers,
                };
                SqlCmd.Parameters.Add(Count_users_registers);

                SqlParameter Count_users_registers_masculine = new()
                {
                    ParameterName = "Count_users_registers_masculine",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_registers_masculine,
                };
                SqlCmd.Parameters.Add(Count_users_registers_masculine);

                SqlParameter Count_users_registers_female = new()
                {
                    ParameterName = "Count_users_registers_female",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_registers_female,
                };
                SqlCmd.Parameters.Add(Count_users_registers_female);

                SqlParameter Percent_users_registers_female = new()
                {
                    ParameterName = "Percent_users_registers_female",
                    SqlDbType = SqlDbType.Decimal,
                    Value = user.Percent_users_registers_female,
                };
                SqlCmd.Parameters.Add(Percent_users_registers_female);

                SqlParameter Percent_users_registers_masculine = new()
                {
                    ParameterName = "Percent_users_registers_masculine",
                    SqlDbType = SqlDbType.Decimal,
                    Value = user.Percent_users_registers_masculine,
                };
                SqlCmd.Parameters.Add(Percent_users_registers_masculine);

                SqlParameter Count_users_actives = new()
                {
                    ParameterName = "Count_users_actives",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_actives,
                };
                SqlCmd.Parameters.Add(Count_users_actives);

                SqlParameter Count_users_actives_masculine = new()
                {
                    ParameterName = "Count_users_actives_masculine",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_actives_masculine,
                };
                SqlCmd.Parameters.Add(Count_users_actives_masculine);

                SqlParameter Count_users_actives_female = new()
                {
                    ParameterName = "Count_users_actives_female",
                    SqlDbType = SqlDbType.Int,
                    Value = user.Count_users_actives_female,
                };
                SqlCmd.Parameters.Add(Count_users_actives_female);

                SqlParameter Percent_users_actives_masculine = new()
                {
                    ParameterName = "Percent_users_actives_masculine",
                    SqlDbType = SqlDbType.Decimal,
                    Value = user.Percent_users_actives_masculine,
                };
                SqlCmd.Parameters.Add(Percent_users_actives_masculine);

                SqlParameter Percent_users_actives_female = new()
                {
                    ParameterName = "Percent_users_actives_female",
                    SqlDbType = SqlDbType.Decimal,
                    Value = user.Percent_users_actives_female,
                };
                SqlCmd.Parameters.Add(Percent_users_actives_female);
               
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;

                user.Id_users_gender = Convert.ToInt32(SqlCmd.Parameters["@Id_users_deserter"].Value);
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
