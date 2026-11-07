using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace DVLD.GLobal_Classes
{
    internal class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUser(string usernamem, string password)
        {
            try
            {
                string FilePath = Path.Combine(Application.StartupPath, "RememberMe.txt");

                if (usernamem == "" && File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return true;
                }

                string DataToSave = usernamem + "#//#" + password;

                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.WriteLine(DataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user credentials: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool RememberUserUsingRegistry(string username, string password)
        {

            if (username == "")
            {
                try
                {
                    // Open the registry key in read/write mode with explicit registry view
                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                    {
                        using (RegistryKey key = baseKey.OpenSubKey(@"SOFTWARE\DVLD", true))
                        {
                            if (key != null)
                            {
                                // Delete the specified value
                                key.DeleteValue("Username", false);
                                key.DeleteValue("Password", false);

                                return true;
                            }
                           
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("UnauthorizedAccessException: Run the program with administrative privileges.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\DVLD");
                key.SetValue("Username", username);
                key.SetValue("Password", password);
                key.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user credentials to registry: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool GetUserCredentialsUsingRegistry(ref string username, ref string password)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DVLD");
                username = key.GetValue("Username", username) as string;
                if (username == null)
                    return false;
                else
                {
                    password = key.GetValue("Password", password) as string;
                    key.Close();
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user credentials to registry: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool GetUserCredentials(ref string username, ref string password)
        {
            try
            {
                string FilePath = Path.Combine(Application.StartupPath, "RememberMe.txt");


                if (File.Exists(FilePath))
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string line = string.Empty;

                        if ((line = reader.ReadLine()) != null)
                        {
                            string[] results = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            username = results[0];
                            password = results[1];
                        }

                        return true;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving user credentials: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }
    
    
    }

}
