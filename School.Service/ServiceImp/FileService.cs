using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using School.Data.Constants;
using School.Service.IService;

namespace School.Service.ServiceImp
{
    public class FileService(IWebHostEnvironment _webHostEnvironment) : IFileService
    {

        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            var path = _webHostEnvironment.WebRootPath + "/" + Location + "/";

            var extention = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extention;
            if (file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestreem = File.Create(path + fileName))
                    {
                        await file.CopyToAsync(filestreem);
                        await filestreem.FlushAsync();
                        return $"/{Location}/{fileName}";
                    }
                }
                catch (Exception)
                {
                    return UplaodingImageState.Error;
                }
            }
            else
            {
                return UplaodingImageState.NoImage;
            }
        }

    }
}
