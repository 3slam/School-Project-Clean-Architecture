using Microsoft.AspNetCore.Http;

namespace School.Service.IService
{
    public interface IFileService
    {
        public Task<string> UploadImage(string Location, IFormFile file);
    }
}
