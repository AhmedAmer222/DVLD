using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.GLobal_Classes
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool CreateFolderIdNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch
                {
                    MessageBox.Show("Error creating folder: " + FolderPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        public static string ReplaceFileNameWithGUID(string SourceFile)
        {
            string FileExtension = Path.GetExtension(SourceFile);

            return Path.Combine(GenerateGUID() + FileExtension);
        }

        public static bool CopyImageToProjectImagesFolder(ref string imagePath)
        {
            string AppImagesFolder = Application.StartupPath + @"\ProfileImages";

            if (!CreateFolderIdNotExist(AppImagesFolder))
                return false;

            string NewImagePath = Path.Combine(AppImagesFolder, ReplaceFileNameWithGUID(imagePath));

            try
            {
                File.Copy(imagePath, NewImagePath);
                imagePath = NewImagePath;
                return true;

            }
            catch (IOException ex)
            {
                MessageBox.Show("Error copying image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }



        public static bool HasValidationErrors(ErrorProvider errp,Control Parient)
        {
            foreach (Control child in Parient.Controls)
            {
                if (errp.GetError(child) != "")
                {
                    return true;
                }

                if (child.HasChildren)
                {
                    if (HasValidationErrors(errp, child))
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        public static string GetHasedPassword(string password)
        {

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                byte[] hash = sha256.ComputeHash(bytes);

                password = BitConverter.ToString(hash).Replace("-", "");

            }

            return password;
        }

    }
}
