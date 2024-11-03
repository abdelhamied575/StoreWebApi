using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Services.Helper
{
    public class DocumentSettings
    {

        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. File Location Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", folderName);

            // 2. Get FileName and Make it Unique
            var fileName = $"{Guid.NewGuid().ToString()}-{Path.GetFileName(file.FileName)}";

            // 3. Get The File Path
            var filePath= Path.Combine(folderPath, fileName);

            // 4. use file stream to make a copy
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return $"images/{folderName}/{fileName}";

        }

        
        public static bool DeleteFile(string ImageUrl,string FolderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/");

            var filePath = Path.Combine(folderPath, ImageUrl);

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                return true ;
            }
            return false ;
        
        }






    }
}
