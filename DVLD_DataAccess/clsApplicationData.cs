using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Applications";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader =  command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }


            return dt;
        }

        public static bool GetApplicationById(int id, ref int AppPersonId, ref DateTime AppDate,
            ref int AppTypeId, ref byte AppStatus, ref DateTime LastStatusDate, 
            ref float PaidFees, ref int UserId)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Applications
                             where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    AppPersonId = (int)reader["ApplicantPersonID"];
                    AppDate = (DateTime)reader["ApplicationDate"];
                    AppTypeId = (int)reader["ApplicationTypeID"];
                    AppStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    UserId = (int)reader["CreatedByUserID"];
                }

                reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine (ex.Message);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;    
        }

        public static bool IsApplicationExist(int AppId)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Found = 1 from Applications
                            where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand (query, connection);
            command.Parameters.AddWithValue ("@ApplicationID", AppId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
   
    
        public static bool UpdateApplication(int id, int AppPersonId, DateTime AppDate,
            int AppTypeId, int AppStatus, DateTime LastStatusDate,
            float PaidFees, int UserId)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"update Applications
                            set ApplicantPersonID = @ApplicantPersonID,
                            	ApplicationDate = @ApplicationDate,
                            	ApplicationTypeID = @ApplicationTypeID,
                            	ApplicationStatus = @ApplicationStatus,
                            	LastStatusDate = @LastStatusDate,
                            	PaidFees = @PaidFees,
                            	CreatedByUserID = @CreatedByUserID
                            where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", id);
            command.Parameters.AddWithValue("@ApplicantPersonID", AppPersonId);
            command.Parameters.AddWithValue("@ApplicationDate", AppDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", AppTypeId);
            command.Parameters.AddWithValue("@ApplicationStatus", AppStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", UserId);


            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                RowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static int AddApplication(int AppPersonId, DateTime AppDate,
            int AppTypeId, int AppStatus, DateTime LastStatusDate,
            float PaidFees, int UserId)
        {
            int NewId = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Applications
                            (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, 
                             LastStatusDate, PaidFees, CreatedByUserID)
                            VALUES
                            (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, 
                             @LastStatusDate, @PaidFees, @CreatedByUserID);

                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", AppPersonId);
            command.Parameters.AddWithValue("@ApplicationDate", AppDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", AppTypeId);
            command.Parameters.AddWithValue("@ApplicationStatus", AppStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", UserId);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertdID))
                {
                    NewId = insertdID;
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
                NewId = 0;
            }
            finally
            {
                connection.Close();
            }

            return NewId;
        }

        public static bool UpdateStatus(int id, byte AppStatus)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"update Applications
                            set                             	                          	
                           	ApplicationStatus = @ApplicationStatus,
                            	LastStatusDate = @LastStatusDate
                            where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", id);
            command.Parameters.AddWithValue("@ApplicationStatus", AppStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                RowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);
        }

        public static bool DeleteApplication(int id)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM Applications
                            WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", id);
            

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                RowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);
        }














































    }
}
