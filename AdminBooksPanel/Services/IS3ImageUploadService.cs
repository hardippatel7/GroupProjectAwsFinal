using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PhotoAlbumApi.Services
{
    public interface IS3ImageUploadService
    {
        Task<string>  UploadImageToS3(IFormFile file, string fileName);

        Task DeleteImageFromS3(string fileName);
        Task<string> FilUpload(IFormFile file);
    }
}
