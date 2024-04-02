using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace ECommerceApi.Application.Helper
{
    public class Files
    {
        public const string masterFolderName = "Resources";
        public static string UploadFiles(string folderName, IFormFile image)
        {
            folderName = Path.Combine(masterFolderName, folderName);
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (image.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName!.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = DateTime.Now.Ticks + fileName;
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                return dbPath;
            }
            else
            {
                return "Error while uploading image";
            }
        }
    }
}
