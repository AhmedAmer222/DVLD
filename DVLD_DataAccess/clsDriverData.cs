using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {
 
        public static bool GetDriverByID(int driverID, ref int personID,
            ref int createdByUserID, ref DateTime createdDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Drivers
                            WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", driverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    personID = (int)reader["PersonID"];
                    createdByUserID = (int)reader["CreatedByUserID"];
                    createdDate = (DateTime)reader["CreatedDate"];
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

            return isFound;
        }

        public static bool GetDriverByPersonID(ref int driverID, int personID,
            ref int createdByUserID, ref DateTime createdDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Drivers
                            WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    driverID = (int)reader["DriverID"];
                    createdByUserID = (int)reader["CreatedByUserID"];
                    createdDate = (DateTime)reader["CreatedDate"];
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

            return isFound;
        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select distinct Drivers.DriverID, People.PersonID, People.NationalNo,
                             FullName = People.FirstName + People.SecondName + ISNULL(People.ThirdName, ''), People.LastName,
                             Drivers.CreatedDate, 
                             (select count(Licenses.IsActive) from Licenses
                             where Licenses.IsActive = 1 and Licenses.DriverID = Drivers.DriverID) as AvtiveLicenses
                             from Drivers
                             inner join People on Drivers.PersonID = People.PersonID
                             inner join Licenses on Drivers.DriverID = Licenses.DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
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

        public static int AddDriver(int personID, int createdByUserID, DateTime createdDate)
        {
            int newID = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                            VALUES (@PersonID, @CreatedByUserID, @CreatedDate);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
            command.Parameters.AddWithValue("@CreatedDate", createdDate);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    newID = id;
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

            return newID;
        }

        public static bool UpdateDriver(int driverID, int personID,
            int createdByUserID, DateTime createdDate)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Drivers SET
                            PersonID        = @PersonID,
                            CreatedByUserID = @CreatedByUserID,
                            CreatedDate     = @CreatedDate
                            WHERE DriverID  = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", driverID);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
            command.Parameters.AddWithValue("@CreatedDate", createdDate);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isUpdated = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isUpdated;
        }

        public static bool DeleteDriver(int driverID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", driverID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isDeleted;
        }

        public static bool IsDriverExist(int driverID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT FOUND = 1 FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", driverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isExist = reader.HasRows;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isExist;
        }

        public static bool IsDriverExistByPersonID(int personID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isExist = reader.HasRows;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isExist;
        }


        public static bool DoesPersonHaveActiveLicenseForLicenseClass(int personID, int licenseClassID)
        {
            bool hasLicense = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT FOUND = 1 FROM Drivers
                             INNER JOIN Licenses ON Drivers.DriverID = Licenses.DriverID
                             WHERE Licenses.LicenseClassID = @LicenseClassID
                             AND Drivers.PersonID          = @PersonID
                             AND Licenses.IsActive         = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);

            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                hasLicense = count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return hasLicense;
        }

   
        public static bool DoesHasActiveLicenseForLicenseClass(int driverID, int licenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Licenses.LicenseID from Licenses
                             inner join Drivers on Licenses.DriverID = Drivers.DriverID
                             where Licenses.LicenseClass = @LicenseClass and Licenses.IsActive = 1
                             and Drivers.DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", driverID);
            command.Parameters.AddWithValue("@LicenseClass", licenseClassID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
               
                IsFound = reader.HasRows;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }


            return IsFound;
        }
    }
}
