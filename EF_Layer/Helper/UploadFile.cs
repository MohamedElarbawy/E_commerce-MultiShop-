using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Helper
{
    public static class UploadFile
    {
        public static string SaveFile(IFormFile file,string folderPath)
        {
            try { 
            //Get directory
            string FilePath = Directory.GetCurrentDirectory() + "/wwwroot/" + folderPath;
            //get file name
            string FileName= Guid.NewGuid()+ Path.GetFileName(file.FileName);
            //merge directory with file name
            string FinalPath= Path.Combine(FilePath,FileName);

            //save file as stream
            using(var stream =new FileStream(FinalPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return FileName;
            }
            catch { return ""; }

        }

        public static void RemoveFile(string FolderName, string RemovedFileName)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/" + FolderName + RemovedFileName))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/" + FolderName + RemovedFileName);
            }

        }
    }
}
