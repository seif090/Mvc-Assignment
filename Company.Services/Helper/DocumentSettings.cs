using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // Check if the file is not null and has content
            if (file == null || file.Length == 0)
            {
                return "File is empty or null";
            }

            // Define the folder path to save the uploaded file (ensure folder exists)
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",folderName);

            // Create directory if it doesn't exist
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Create the full file path
            string filePath = Path.Combine(uploadPath, file.FileName);

            try
            {
                // Save the file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Return the file path or a success message
                return file.FileName;
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur
                return $"File upload failed: {ex.Message}";
            }
        }
    }
}
