using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Policy;


namespace DVLD_DataAccess
{
    public  class clsApplicationTypeData
    {
        
        public static DataTable GetAllAplicationTypes()
        {
            DataTable result = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes";

            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    result.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
    
        public static bool GetApplicationTypeByID(int id, ref string AppTitle, ref float AppFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT ApplicationTypeTitle, ApplicationFees 
                             FROM ApplicationTypes 
                             WHERE ApplicationTypeID = @AppID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    AppTitle = reader["ApplicationTypeTitle"].ToString();
                    AppFees = Convert.ToSingle(reader["ApplicationFees"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        public static int AddApplicationType(string AppTitle, float AppFees)
        {
            int NewAppTypeID = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO ApplicationTypes (ApplicationTypeTitle, ApplicationFees)
                            Values(@AppTitle, @AppFees )
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppTitle", AppTitle);
            command.Parameters.AddWithValue("@AppFees", AppFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    NewAppTypeID = insertedID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }


            return NewAppTypeID;
        }

        public static bool UpdataApplicationType(int AppID, string AppTitle, float AppFees)
        {
            int RowsAffected = 0;   

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE ApplicationTypes 
                            SET ApplicationTypeTitle = @AppTitle,
                                ApplicationFees = @AppFees 
                            WHERE ApplicationTypeID = @AppID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            command.Parameters.AddWithValue("@AppTitle", AppTitle);
            command.Parameters.AddWithValue("@AppFees", AppFees);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }


            return (RowsAffected > 0);
        }
    
        
    
    
    
    
    
    }
}
