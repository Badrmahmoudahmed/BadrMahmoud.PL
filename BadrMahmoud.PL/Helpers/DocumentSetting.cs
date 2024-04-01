using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace BadrMahmoud.PL.Helpers
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file , string foldername)
        {
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", foldername);

            if (!Directory.Exists(folderpath)) 
            {
                Directory.CreateDirectory(folderpath);
            }

            string filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            string filepath = Path.Combine(folderpath,filename);

            using var filestream = new FileStream(filepath, FileMode.Create);

            file.CopyTo(filestream);

            return filename;
        }

        public static void DeleteFile(string filename , string foldername) 
        {
            if(filename is not null && foldername is not null)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", foldername, filename);
                if (Directory.Exists(filepath))
                {
                    Directory.Delete(filepath);
                }
            }
        }
    }
}
