using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;


namespace DVLD_DataAccess
{
    public class clsPersonData
    {

        public static DataTable GatAllPeople()
        {
            DataTable AllPeople = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select p.PersonID,P.NationalNo, p.FirstName, p.SecondName, p.ThirdName, p.LastName,
                            p.DateOfBirth, 
                            case
                                when p.Gendor = 0 then 'Male'
                                when p.Gendor = 1 then 'Female'
                                else '-----'
                            end as Gender,
                            p.Address, p.Phone, p.Email, c.CountryName, p.ImagePath

                            from People p Inner Join Countries c
                            on p.NationalityCountryID = c.CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    AllPeople.Load(reader);
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

            return AllPeople; 
        }   
        
        
        
        public static bool GetPersonByID(int id, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThridName,
            ref string LastName, ref DateTime Date, ref short Gender, ref string Address, ref string Phone,
            ref string Email, ref string NationalityCountryID, ref string NationalityCountryName, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select p.*, c.CountryName
                             from People p inner join Countries c
                             on p.NationalityCountryID = c.CountryID and p.PersonID = @PersonID";
            
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", id);

            bool IsFound = false;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    NationalNo = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();

                    if(reader["ThirdName"] != DBNull.Value)
                    {
                        ThridName = reader["ThirdName"].ToString();
                    }

                    
                    LastName = reader["LastName"].ToString();
                    Date = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gender = (byte)reader["Gendor"];
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = reader["Email"].ToString();
                    }        

                    NationalityCountryID = reader["NationalityCountryID"].ToString();
                    NationalityCountryName = reader["CountryName"].ToString();

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = reader["ImagePath"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        public static bool GetPersonByNationalNo(ref int id,string NationalNo, ref string FirstName, ref string SecondName, ref string ThridName,
            ref string LastName, ref DateTime Date, ref short Gender, ref string Address, ref string Phone,
            ref string Email, ref string NationalityCountryID, ref string NationalityCountryName, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select p.*, c.CountryName
                             from People p inner join Countries c
                             on p.NationalityCountryID = c.CountryID and p.NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            bool IsFound = false;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    id = (int)reader["PersonID"];
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThridName = reader["ThirdName"].ToString();
                    }


                    LastName = reader["LastName"].ToString();
                    Date = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gender = (byte)reader["Gendor"];
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = reader["Email"].ToString();
                    }

                    NationalityCountryID = reader["NationalityCountryID"].ToString();
                    NationalityCountryName = reader["CountryName"].ToString();

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = reader["ImagePath"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        public static bool IsPersonExistByID(int id)
        {
            bool IsExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT FirstName FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
               
                IsExist = reader.HasRows;

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

            return IsExist;
        }

        public static bool IsPersonExistByNationalNo(string NationalNo)
        {
            bool IsExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT FirstName FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                IsExist = reader.HasRows;

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

            return IsExist;
        }


        public static bool DeletePersonByID(int id)
        {
            bool IsDeleted = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", id);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    IsDeleted = true;
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


            return IsDeleted;
        }
    
    
        public static int AddPerson(string NationalNo, string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime Date, short Gender, string Address, string Phone, string Email, string NationalityCountryID, string ImagePath)
        {
            int ContactID = 0;

            

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth,
                            Gendor, Address, Phone, Email, NationalityCountryID, ImagePath)
                            VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth,
                            @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (!string.IsNullOrEmpty(ThirdName))
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }
            else
            {
                command.Parameters.AddWithValue("@ThridName", DBNull.Value);
            }

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", Date);
            command.Parameters.AddWithValue("@Gendor", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (!string.IsNullOrEmpty(Email))
            {
                command.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (!string.IsNullOrEmpty(ImagePath))
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int newContactID))
                {
                    ContactID = newContactID;
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
            

            return ContactID;
        }

           

        public static bool UpdatePerson(int id, string NationalNo, string FirstName, string SecondName, string ThridName,
            string LastName, DateTime Date, short Gender, string Address, string Phone, string Email, string NationalityCountryID, string ImagePath)
        {
            bool isUpdated = false;

          

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE People 
                            SET NationalNo = @NationalNo, 
                            FirstName = @FirstName,
                            SecondName = @SecondName, 
                            ThirdName = @ThridName, 
                            LastName = @LastName,
                            DateOfBirth = @Date,
                            Gendor = @Gender,
                            Address = @Address,
                            Phone = @Phone,
                            Email = @Email,
                            NationalityCountryID = @NationalityCountryID,
                            ImagePath = @ImagePath
                            WHERE PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", id);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (!string.IsNullOrEmpty(ThridName))
            {
                command.Parameters.AddWithValue("@ThridName", ThridName);
            }
            else
            {
                command.Parameters.AddWithValue("@ThridName", DBNull.Value);
            }

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Date", Date);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (!string.IsNullOrEmpty(Email))
            {
                command.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (!string.IsNullOrEmpty(ImagePath))
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }


            try
            {
                connection.Open();

                int RowsAffected = command.ExecuteNonQuery();

                isUpdated = RowsAffected > 0;
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
