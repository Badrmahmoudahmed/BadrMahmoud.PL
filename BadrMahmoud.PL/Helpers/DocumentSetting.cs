using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BadrMahmoud.PL.Helpers
{
    public static class DocumentSetting
    {
        public static async Task<string> UploadFile(IFormFile file , string foldername)
        {
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", foldername);

            if (!Directory.Exists(folderpath)) 
            {
                Directory.CreateDirectory(folderpath);
            }

            string filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            string filepath = Path.Combine(folderpath,filename);

            using var filestream = new FileStream(filepath, FileMode.Create);

            await file.CopyToAsync(filestream);

            return filename;
        }

        public static void DeleteFile(string filename , string foldername) 
        {
            if(filename is not null && foldername is not null)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", foldername, filename);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
        }
    }
}
