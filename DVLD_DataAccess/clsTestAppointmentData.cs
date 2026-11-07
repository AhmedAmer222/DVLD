using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM TestAppointments_View";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    dt.Load(reader);
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

            return dt;
        }       
       
        public static DataTable GetAllTestAppointmentsForSpecificTest(int localAppid, int testTypeId)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select TestAppointments.TestAppointmentID , TestAppointments.AppointmentDate,
                             TestAppointments.PaidFees, TestAppointments.IsLocked
                             from TestAppointments
                             inner join TestTypes on TestAppointments.TestTypeID = TestTypes.TestTypeID
                             where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                             and TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localAppid);
            command.Parameters.AddWithValue("@TestTypeID", testTypeId);

            try
            {
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;

        }


        public static int GetTotelTrial(int localAppid, int testTypeId)
        {
            int totalTrial = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(TestAppointmentID) from TestAppointments
                            where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                            and TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localAppid);
            command.Parameters.AddWithValue("@TestTypeID", testTypeId);

            try
            {
                conn.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int total))
                {
                    totalTrial = total;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return totalTrial;

        }

        public static bool GetTestAppointmentByID(int testAppointmentID, ref int testTypeID,
            ref int localDrivingLicenseApplicationID, ref DateTime appointmentDate,
            ref float paidFees, ref int createdByUserID, ref bool isLocked,
            ref int retakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM TestAppointments 
                    WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testTypeID = (int)reader["TestTypeID"];
                    localDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    appointmentDate = (DateTime)reader["AppointmentDate"];
                    paidFees = Convert.ToSingle(reader["PaidFees"]);
                    createdByUserID = (int)reader["CreatedByUserID"];
                    isLocked = (bool)reader["IsLocked"];
                    retakeTestApplicationID = reader["RetakeTestApplicationID"] != DBNull.Value
                        ? (int)reader["RetakeTestApplicationID"] : 0; // ✅ handle null
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

        public static bool GetLastTestAppointment(ref int testAppointmentID, int testTypeID,
                 int localDrivingLicenseApplicationID, ref DateTime appointmentDate,
                 ref float paidFees, ref int createdByUserID, ref bool isLocked,
                 ref int retakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select top 1* from TestAppointments
                             where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                                and TestTypeID = @TestTypeID
                             order by TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testAppointmentID = (int)reader["TestAppointmentID"];
                    appointmentDate = (DateTime)reader["AppointmentDate"];
                    paidFees = Convert.ToSingle(reader["PaidFees"]);
                    createdByUserID = (int)reader["CreatedByUserID"];
                    isLocked = (bool)reader["IsLocked"];
                    retakeTestApplicationID = reader["RetakeTestApplicationID"] != DBNull.Value
                        ? (int)reader["RetakeTestApplicationID"] : 0; // ✅ handle null
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


        public static bool IsTestAppointmentExist(int testAppointmentID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT FOUND = 1 FROM TestAppointments 
                             WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                isExist = count > 0;
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


        public static int AddTestAppointment(int testTypeID,
                int localDrivingLicenseApplicationID, DateTime appointmentDate,
                float paidFees, int createdByUserID, bool isLocked,
                int retakeTestApplicationID)
        {
            int newID = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO TestAppointments 
                    (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,
                    PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
                    VALUES
                    (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate,
                    @PaidFees, @CreatedByUserID, @IsLocked, @RetakeTestApplicationID);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", testTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
            command.Parameters.AddWithValue("@PaidFees", paidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
            command.Parameters.AddWithValue("@IsLocked", isLocked);

            // ✅ handle optional retake application ID
            if (retakeTestApplicationID != 0)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", retakeTestApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);

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

        public static bool UpdateTestAppointment(int testAppointmentID, int testTypeID,
            int localDrivingLicenseApplicationID, DateTime appointmentDate,
            float paidFees, int createdByUserID, bool isLocked,
            int retakeTestApplicationID)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE TestAppointments 
                            SET
                                TestTypeID = @TestTypeID,
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                AppointmentDate = @AppointmentDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID = @CreatedByUserID,
                                IsLocked = @IsLocked,
                                RetakeTestApplicationID = @RetakeTestApplicationID

                            WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
            command.Parameters.AddWithValue("@PaidFees", paidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
            command.Parameters.AddWithValue("@IsLocked", isLocked);

            if (retakeTestApplicationID != 04)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", retakeTestApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);

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

        public static bool DeleteTestAppointment(int testAppointmentID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

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


        public static bool LockTestAppointment(int testAppointmentID)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE TestAppointments 
                            SET
                                IsLocked = 1
                            WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);


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
