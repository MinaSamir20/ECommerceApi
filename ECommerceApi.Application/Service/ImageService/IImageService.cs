using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Application.Service.ImageService
{
    public interface IImageService
    {
        public string UploadImage(IFormFile file, string path);
        public byte[] GetImage(string imageName, string path);
    }
}
