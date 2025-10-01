using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        public string? Upload(IFormFile file, string FolderName)
        {
            List<string> AllowedExtension = [".png", ".jpg", ".jpeg"];
            const int MaxSize = 2_097_152;
            //1-check Extension
            var Extension = Path.GetExtension(file.FileName);
            if (!AllowedExtension.Contains(Extension)) return null;
            
            //2-check size
            if(file.Length==0 || file.Length==MaxSize) return null;

            //3-Get Located folder path
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            //4-make Attachment Unique {guid}
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            //5-Get file path
            var filePath= Path.Combine(FolderPath, fileName);

            //6-create file stream to copy file [unmanaged]
            using FileStream FS = new FileStream(filePath, FileMode.Create);

            //7-use stream to copy file
            file.CopyTo(FS);

            //8-return fileName to store in Datebase
            return fileName;
        }

        public bool Delete(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
            }
            return true;
        }
    }
}
