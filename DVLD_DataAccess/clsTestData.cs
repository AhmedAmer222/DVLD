using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsTestData
    {

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Tests";

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

        public static int GetTotalPassedTestsCount(int localAppId)
        {
            int totalPassedTestsCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(TestResult) from Tests 
                             inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                             where TestAppointments.LocalDrivingLicenseApplicationID = 44
                             and Tests.TestResult = 1";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int totalTests))
                {
                    totalPassedTestsCount = totalTests;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return totalPassedTestsCount;
        }

        public static bool GetTestByID(int testID, ref int testAppointmentID,
            ref bool testResult, ref string notes, ref int createdByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Tests 
                            WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", testID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testAppointmentID = (int)reader["TestAppointmentID"];
                    testResult = (bool)reader["TestResult"];
                    createdByUserID = (int)reader["CreatedByUserID"];

                    // ✅ Notes is nullable
                    if (reader["Notes"] != DBNull.Value)
                        notes = reader["Notes"].ToString();
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


        public static bool GetTestByTestAppointmentID(ref int testID, int testAppointmentID,
           ref bool testResult, ref string notes, ref int createdByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Tests
                             where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testID = (int)reader["TestID"];
                    testResult = (bool)reader["TestResult"];
                    createdByUserID = (int)reader["CreatedByUserID"];

                    // ✅ Notes is nullable
                    if (reader["Notes"] != DBNull.Value)
                        notes = reader["Notes"].ToString();
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

        public static bool IsTestExist(int testID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT FOUND = 1 FROM Tests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", testID);

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

        

        public static int AddTest(int testAppointmentID, bool testResult,
            string notes, int createdByUserID)
        {
            int newID = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Tests
                            (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                            VALUES
                            (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);
            command.Parameters.AddWithValue("@TestResult", testResult);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

            if (!string.IsNullOrEmpty(notes))
                command.Parameters.AddWithValue("@Notes", notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

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

        public static bool UpdateTest(int testID, int testAppointmentID,
            bool testResult, string notes, int createdByUserID)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Tests 
                            SET
                                TestAppointmentID = @TestAppointmentID,
                                TestResult        = @TestResult,
                                Notes             = @Notes,
                                CreatedByUserID   = @CreatedByUserID
                            WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", testID);
            command.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);
            command.Parameters.AddWithValue("@TestResult", testResult);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

            if (!string.IsNullOrEmpty(notes))
                command.Parameters.AddWithValue("@Notes", notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

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

        public static bool DeleteTest(int testID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Tests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", testID);

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


        public static int GetPassedTest(int LocalAppId)
        {
            int passed = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select count(ldl.LocalDrivingLicenseApplicationID) from LocalDrivingLicenseApplications ldl
                             inner join TestAppointments ta on ldl.LocalDrivingLicenseApplicationID = ta.LocalDrivingLicenseApplicationID
                             inner join Tests on Tests.TestAppointmentID = ta.TestAppointmentID
                             where Tests.TestResult = 1 and ldl.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalAppId);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int TestPassed))
                {
                    passed = TestPassed;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return passed;
        }
       
    }
}
