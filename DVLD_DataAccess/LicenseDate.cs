using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenseData
    {
        public static bool GetLicenseByID(int licenseID, ref int applicationID,
            ref int driverID, ref int licenseClassID, ref DateTime issueDate,
            ref DateTime expirationDate, ref string notes, ref float paidFees,
            ref bool isActive, ref int issueReason, ref int createdByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Licenses
                            WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", licenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    applicationID = (int)reader["ApplicationID"];
                    driverID = (int)reader["DriverID"];
                    licenseClassID = (int)reader["LicenseClass"];
                    issueDate = (DateTime)reader["IssueDate"];
                    expirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] != DBNull.Value)
                        notes = reader["Notes"].ToString();

                    paidFees = Convert.ToSingle(reader["PaidFees"]);
                    isActive = (bool)reader["IsActive"];
                    issueReason = (byte)reader["IssueReason"];
                    createdByUserID = (int)reader["CreatedByUserID"];



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

        public static bool GetLicenseByApplicationID(ref int licenseID, int applicationID,
            ref int driverID, ref int licenseClassID, ref DateTime issueDate,
            ref DateTime expirationDate, ref string notes, ref float paidFees,
            ref bool isActive, ref int issueReason, ref int createdByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Licenses
                            WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", applicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    licenseID = (int)reader["LicenseID"];
                    driverID = (int)reader["DriverID"];
                    licenseClassID = (int)reader["LicenseClass"];
                    issueDate = (DateTime)reader["IssueDate"];
                    expirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] != DBNull.Value)
                        notes = reader["Notes"].ToString();

                    paidFees = Convert.ToSingle(reader["PaidFees"]);
                    isActive = (bool)reader["IsActive"];
                    issueReason = (byte)reader["IssueReason"];
                    createdByUserID = (int)reader["CreatedByUserID"];



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


        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Licenses";

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

        public static DataTable GetAllLicensesForPerson(int personId)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, 
                             Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
                             from Licenses
	                         inner join Applications on Licenses.ApplicationID = Applications.ApplicationID
                             inner join LicenseClasses on Licenses.LicenseClass = LicenseClasses.LicenseClassID
                             inner join Drivers on Licenses.DriverID = Drivers.DriverID
                             where Drivers.PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personId);

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




        public static bool GetActiveLicenseByDriverIDAndClass(int driverID,
            int licenseClassID, ref int licenseID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT LicenseID FROM Licenses
                            WHERE DriverID      = @DriverID
                            AND LicenseClass  = @LicenseClass
                            AND IsActive        = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", driverID);
            command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    licenseID = (int)reader["LicenseID"];
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

        public static int AddLicense(int applicationID, int driverID,
            int licenseClassID, DateTime issueDate, DateTime expirationDate,
            string notes, float paidFees, bool isActive, int issueReason,
            int createdByUserID)
        {
            int newID = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Licenses
                            (ApplicationID, DriverID, LicenseClass, IssueDate,
                            ExpirationDate, Notes, PaidFees, IsActive,  IssueReason,
                            CreatedByUserID)
                            VALUES
                            (@ApplicationID, @DriverID, @LicenseClassID, @IssueDate,
                            @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason,
                            @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", applicationID);
            command.Parameters.AddWithValue("@DriverID", driverID);
            command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);
            command.Parameters.AddWithValue("@IssueDate", issueDate);
            command.Parameters.AddWithValue("@ExpirationDate", expirationDate);

            if (!string.IsNullOrEmpty(notes))
                command.Parameters.AddWithValue("@Notes", notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", paidFees);
            command.Parameters.AddWithValue("@IsActive", isActive);
            command.Parameters.AddWithValue("@IssueReason", issueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);


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

        public static bool UpdateLicense(int licenseID, int applicationID,
            int driverID, int licenseClassID, DateTime issueDate,
            DateTime expirationDate, string notes, float paidFees,
            bool isActive, int issueReason, int createdByUserID)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses 
                            SET
                                ApplicationID  = @ApplicationID,
                                DriverID       = @DriverID,
                                LicenseClass = @LicenseClassID,
                                IssueDate      = @IssueDate,
                                ExpirationDate = @ExpirationDate,
                                Notes          = @Notes,
                                PaidFees       = @PaidFees,
                                IsActive       = @IsActive,
                                IssueReason    = @IssueReason,
                                CreatedByUserID = @CreatedByUserID
                            WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", licenseID);
            command.Parameters.AddWithValue("@ApplicationID", applicationID);
            command.Parameters.AddWithValue("@DriverID", driverID);
            command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);
            command.Parameters.AddWithValue("@IssueDate", issueDate);
            command.Parameters.AddWithValue("@ExpirationDate", expirationDate);

            if (!string.IsNullOrEmpty(notes))
                command.Parameters.AddWithValue("@Notes", notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", paidFees);
            command.Parameters.AddWithValue("@IsActive", isActive);
            command.Parameters.AddWithValue("@IssueReason", issueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);



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

        public static bool DeleteLicense(int licenseID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", licenseID);

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

        public static bool IsLicenseExist(int licenseID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", licenseID);

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

        public static bool DeactivateLicense(int licenseID)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses SET IsActive = 0
                            WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", licenseID);

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
    
    
    
    }
}

