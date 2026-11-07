using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ldl.LocalDrivingLicenseApplicationID, ldl.ApplicationID, p.NationalNo, 
                             FullName = p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName,
                             App.ApplicationDate,
                             PassedTests =  COUNT(CASE WHEN Tests.TestResult = 1 THEN 1 END), 
                              CASE  App.ApplicationStatus
                                     WHEN 1 THEN 'New'
                                     WHEN 2 THEN 'Cancelled'
                                     WHEN 3 THEN 'Completed'
                                     ELSE 'Unknown'
                              END AS ApplicationStatus
                             from LocalDrivingLicenseApplications ldl
                             inner join Applications App on ldl.ApplicationID = App.ApplicationID
                             inner join People p on App.ApplicantPersonID = p.PersonID
                             inner join LicenseClasses lc on ldl.LicenseClassID = lc.LicenseClassID
                             left join TestAppointments ta ON ldl.LocalDrivingLicenseApplicationID = ta.LocalDrivingLicenseApplicationID
                             left join Tests on ta.TestAppointmentID = Tests.TestAppointmentID
                             GROUP BY                          
                                 ldl.LocalDrivingLicenseApplicationID,
                                 ldl.ApplicationID,
                                 p.NationalNo,
                                 p.FirstName, p.SecondName, p.ThirdName, p.LastName,
                                 App.ApplicationDate,
                                 App.ApplicationStatus
                                 order by ldl.LocalDrivingLicenseApplicationID desc";
                             
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
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    
  
        public static bool IslLocalDrivingLicenseApplicationExistByID(int Appid)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from LocalDrivingLicenseApplications
                            where LocalDrivingLicenseApplicationID =  @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", Appid);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                IsFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
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
        public static bool IslLocalDrivingLicenseApplicationExistByApplicationID(int LocalAppid)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from LocalDrivingLicenseApplications
                            where ApplicationID =  @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", LocalAppid);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                IsFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
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


        public static bool GetLocalDrivingLicenseApplicationByID(int LocalAppid, ref int Appid, ref int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ApplicationID, LicenseClassID from LocalDrivingLicenseApplications
                             where LocalDrivingLicenseApplicationID =  @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalAppid);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    Appid = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }

                reader.Close();
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
        public static bool GetLocalDrivingLicenseApplicationByApplicationID(ref int LocalAppid, int Appid, ref int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select LocalDrivingLicenseApplicationID, LicenseClassID from LocalDrivingLicenseApplications
                             where ApplicationID =  @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", Appid);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    LocalAppid = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }

                reader.Close();
            }
            catch (Exception ex)
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


        public static bool DeleteLocalDrivingLicenseApplicationByID(int LocalAppid)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete from LocalDrivingLicenseApplications
                             where LocalDrivingLicenseApplicationID =  @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalAppid);

            try
            {
                connection.Open();

                 RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                RowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }


            return RowsAffected > 0;
        }
        public static bool DeleteLocalDrivingLicenseApplicationByApplicationID(int Appid)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete from LocalDrivingLicenseApplications
                             where ApplicationID =  @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", Appid);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                RowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }


            return RowsAffected > 0;
        }


        public static int AddLocalDrivingLicenseApplication(int AppId, int LiceneseClassId)
        {
            int NewID = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into LocalDrivingLicenseApplications(ApplicationID, LicenseClassID)
                             values(@ApplicationID, @LicenseClassID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", AppId);
            command.Parameters.AddWithValue("@LicenseClassID", LiceneseClassId);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedId))
                {
                    NewID = InsertedId;    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                NewID = 0;
            }
            finally
            {
                connection.Close();
            }

            return NewID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalAppId, int AppId, int LiceneseClassId)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"update LocalDrivingLicenseApplications
                             set
                             	ApplicationID = @ApplicationID,
                             	LicenseClassID = @LicenseClassID
                             where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalAppId);
            command.Parameters.AddWithValue("@ApplicationID", AppId);
            command.Parameters.AddWithValue("@LicenseClassID", LiceneseClassId);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                RowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return RowsAffected > 0;
        }


        public static int DoesHasActiveApplicationForLicenseClass(int PersonId, int LicenseClassId)
        {
            int AppId = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select app.ApplicationID
                             from LocalDrivingLicenseApplications ldla inner join Applications app
                             on ldla.ApplicationID = app.ApplicationID
                             where app.ApplicantPersonID = @ApplicantPersonID
                             and ldla.LicenseClassID = @LicenseClassID
                             and app.ApplicationStatus = 1";
            
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonId);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassId);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ActiveAppId))
                {
                    AppId = ActiveAppId;
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                AppId = 0;
            }
            finally
            {
                connection.Close();
            }
            
            return AppId;
        }



        public static bool DoesPersonHaveLicenseForLicenseClass(int personID, int licenseClassID)
        {
            bool hasLicense = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT FOUND = 1 FROM Drivers
                             INNER JOIN Licenses ON Drivers.DriverID = Licenses.DriverID
                             WHERE Licenses.LicenseClass = @LicenseClassID
                             AND Drivers.PersonID          = @PersonID
                             AND Licenses.IsActive         = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@LicenseClassID", licenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                hasLicense = (result != null);
            
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




    }
}
