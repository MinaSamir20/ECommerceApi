using ECommerceApi.Application.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Application.Service.ImageService
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _web;
        private readonly List<string> _allowedExtentions = new() { ".jpg", ".png", ".jpeg" };
        public ImageService(IWebHostEnvironment web)
        {
            _web = web;
        }
        public string UploadImage(IFormFile file, string path)
        {
            if (file.Length > 3145728)
                return "Not Allowed Size Maximum 3 MB";
            if (path.StartsWith("Resources/Images/"))
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(file!.FileName).ToLower()))
                    return "Only accepts images with the following extensions: .jpg, .png, .jpeg";
                return Files.UploadFiles(_web.ContentRootPath, $"Resources/Images/{path}/", file!);
            }
            return Files.UploadFiles(_web.ContentRootPath, $"Resources/File/", file!);
        }

        public byte[] GetImage(string imageName, string path)
        {
            return Files.GetImage(_web.ContentRootPath, path, imageName);
        }
    }
}
