using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDetainedLicenseData
    {
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM DetainedLicenses_View";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


        public static bool GetDetainedLicenseByID(
            int DetainID,
            ref int LicenseID,
            ref DateTime DetainDate,
            ref float FineFees,
            ref int CreatedByUserID,
            ref bool IsReleased,
            ref DateTime ReleaseDate,
            ref int ReleasedByUserID,
            ref int ReleaseApplicationID)
            {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                     FROM DetainedLicenses
                     WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                    if (reader["ReleaseDate"] != DBNull.Value)
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                    else
                        ReleaseDate = DateTime.MinValue;

                    if (reader["ReleasedByUserID"] != DBNull.Value)
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    else
                        ReleasedByUserID = 0;

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    else
                        ReleaseApplicationID = 0;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool GetDetainedLicenseByLicenseID(
        int LicenseID,
        ref int DetainID,
        ref DateTime DetainDate,
        ref float FineFees,
        ref int CreatedByUserID,
        ref bool IsReleased,
        ref DateTime ReleaseDate,
        ref int ReleasedByUserID,
        ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                     FROM DetainedLicenses
                     WHERE LicenseID = @LicenseID and IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    DetainID = (int)reader["DetainID"];
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                    if (reader["ReleaseDate"] != DBNull.Value)
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                    else
                        ReleaseDate = DateTime.MinValue;

                    if (reader["ReleasedByUserID"] != DBNull.Value)
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    else
                        ReleasedByUserID = 0;

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    else
                        ReleaseApplicationID = 0;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int AddNewDetainedLicense(int LicenseID,
            DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int DetainID = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses
                    (LicenseID, DetainDate, FineFees, CreatedByUserID,
                     IsReleased)
                    VALUES
                    (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID,
                     @IsReleased);

                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", 0);

           

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int NewID))
                {
                    DetainID = NewID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return DetainID;
        }

        public static bool UpdateDetainedLicense(int DetainID,
            int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID)
        {
            bool IsUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses
                            SET LicenseID=@LicenseID,
                                DetainDate=@DetainDate,
                                FineFees=@FineFees,
                                CreatedByUserID=@CreatedByUserID
                            WHERE DetainID=@DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

           

            try
            {
                connection.Open();

                int RowsAffected = command.ExecuteNonQuery();

                IsUpdated = RowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsUpdated;
        }

        public static bool DeleteDetainedLicense(int DetainID)
        {
            bool IsDeleted = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM DetainedLicenses
                     WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                int RowsAffected = command.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    IsDeleted = true;
                }
            }
            catch (Exception ex)
            {
                IsDeleted = false;
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsDeleted;
        }

        public static bool IsDetainedLicenseExist(int DetainID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1
                     FROM DetainedLicenses
                     WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                IsFound = reader.HasRows;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1
                     FROM DetainedLicenses
                     WHERE LicenseID = @LicenseID
                     AND IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                IsFound = reader.HasRows;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool IsLicenseDetainedByDetainID(int DetainID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1
                     FROM DetainedLicenses
                     WHERE DetainID = @DetainID
                     AND IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                IsFound = reader.HasRows;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        public static int GetActiveDetainedLicenseID(int LicenseID)
        {
            int DetainID = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT DetainID
                     FROM DetainedLicenses
                     WHERE LicenseID = @LicenseID
                     AND IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    DetainID = ID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return DetainID;
        }


        public static bool ReleaseDetainedLicense(int DetainID,
            DateTime ReleaseDate,
            int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            bool IsUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses
                     SET IsReleased = 1,
                         ReleaseDate = @ReleaseDate,
                         ReleasedByUserID = @ReleasedByUserID,
                         ReleaseApplicationID = @ReleaseApplicationID
                     WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            try
            {
                connection.Open();

                int RowsAffected = command.ExecuteNonQuery();

                IsUpdated = RowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsUpdated;
        }


        public static bool ReleaseDetainedLicenseByLicenseID(int LicenseID,
            DateTime ReleaseDate,
            int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            bool IsUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses
                     SET IsReleased = 1,
                         ReleaseDate = @ReleaseDate,
                         ReleasedByUserID = @ReleasedByUserID,
                         ReleaseApplicationID = @ReleaseApplicationID
                     WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

            try
            {
                connection.Open();

                int RowsAffected = command.ExecuteNonQuery();

                IsUpdated = RowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsUpdated;
        }

   
    }
}
