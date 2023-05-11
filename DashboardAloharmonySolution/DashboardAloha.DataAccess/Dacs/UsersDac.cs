using DasboardAloha.Entities.ModelsBinding;
using DashboardAloha.DataAccess.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace DashboardAloha.DataAccess.Dacs
{
    public class UsersDac : IUsersDac
    {
        #region CONSTRUCTOR
        private readonly IConnectionDac ConnectionDac;
        public UsersDac(IConnectionDac ConnectionDac)
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

        #region MÉTODO INSERT USER
        public string InsertUser(UserBindingModelModel user)
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
                    CommandText = "sp_Users_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_user = new()
                {
                    ParameterName = "@Id_user",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_user);

                SqlParameter Date_sync_users = new()
                {
                    ParameterName = "@Date_sync_users",
                    SqlDbType = SqlDbType.Date,
                    Value = user.Date_sync_users,
                };
                SqlCmd.Parameters.Add(Date_sync_users);

                SqlParameter Hour_sync_users = new()
                {
                    ParameterName = "@Hour_sync_users",
                    SqlDbType = SqlDbType.Time,
                    Value = user.Hour_sync_users,
                };
                SqlCmd.Parameters.Add(Hour_sync_users);

                SqlParameter Created_user = new()
                {
                    ParameterName = "@Created_user",
                    SqlDbType = SqlDbType.Date,
                    Value = user.Created_user,
                };
                SqlCmd.Parameters.Add(Created_user);

                SqlParameter Email_user = new()
                {
                    ParameterName = "@Email_user",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.Email_user ?? "",
                };
                SqlCmd.Parameters.Add(Email_user);

                SqlParameter Phone = new()
                {
                    ParameterName = "@Phone",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.Phone ?? "",
                };
                SqlCmd.Parameters.Add(Phone);

                SqlParameter Device = new()
                {
                    ParameterName = "@Device",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.Device ?? "",
                };
                SqlCmd.Parameters.Add(Device);

                SqlParameter Full_name = new()
                {
                    ParameterName = "@Full_name",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.FullName ?? "",
                };
                SqlCmd.Parameters.Add(Full_name);

                SqlParameter LastName = new()
                {
                    ParameterName = "@Last_name",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.LastName ?? "",
                };
                SqlCmd.Parameters.Add(LastName);

                SqlParameter Profile = new()
                {
                    ParameterName = "@Profile",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.Profile ?? "",
                };
                SqlCmd.Parameters.Add(Profile);

                SqlParameter Gender = new()
                {
                    ParameterName = "@Gender",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.Gender ?? "",
                };
                SqlCmd.Parameters.Add(Gender);

                SqlParameter Membership = new()
                {
                    ParameterName = "@Membership",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.Membership ?? "",
                };
                SqlCmd.Parameters.Add(Membership);

                SqlParameter Country = new()
                {
                    ParameterName = "@Country",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.Country ?? "",
                };
                SqlCmd.Parameters.Add(Country);

                SqlParameter Status_membership = new()
                {
                    ParameterName = "@Status_membership",
                    SqlDbType = SqlDbType.VarChar,
                    Value = user.Status_membership ?? "",
                };
                SqlCmd.Parameters.Add(Status_membership);

                SqlParameter Total_time_elapsed = new()
                {
                    ParameterName = "@Total_time_elapsed",
                    SqlDbType = SqlDbType.Decimal,
                    Value = user.Total_time_elapsed,
                };
                SqlCmd.Parameters.Add(Total_time_elapsed);
             
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;

                user.Id_user = Convert.ToInt32(SqlCmd.Parameters["@Id_user"].Value);
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

        #region MÉTODO DELETE USERS
        public string DeleteAllUsers()
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
                    CommandText = "sp_Users_d_all",
                    CommandType = CommandType.StoredProcedure
                };
              
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
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
