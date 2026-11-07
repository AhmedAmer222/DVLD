using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestTypeData
    {

        public static DataTable GetAllTestTypes()
        {
            DataTable result = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes";

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

        public static bool GetTestTypeByID(int id, ref string TestTitle, ref string Description, ref float testFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                             FROM TestTypes 
                             WHERE TestTypeID = @testTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeID", id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    TestTitle = reader["TestTypeTitle"].ToString();
                    Description = reader["TestTypeDescription"].ToString();
                    testFees = Convert.ToSingle(reader["TestTypeFees"]);
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


        public static int AddTestType(string TestTitle, string Description, float testFees)
        {
            int NewTestTypeID = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO TestTypes (TestTypeTitle, TestTypeDescription, TestTypeFees)
                            Values(@TestTitle, @Description, @testFees )
                            select SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTitle", TestTitle);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@testFees", testFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result!= null && int.TryParse(result.ToString(), out int insertedID))
                {
                    NewTestTypeID = insertedID;
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


            return NewTestTypeID;
        }

        public static bool UpdataTestType(int id, string TestTitle, string Description, float testFees)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE TestTypes 
                            SET TestTypeTitle = @TestTitle,
                                TestTypeDescription = @Description,
                                TestTypeFees = @testFees 
                            WHERE TestTypeID = @testTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeID", id);
            command.Parameters.AddWithValue("@TestTitle", TestTitle);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@testFees", testFees);

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
