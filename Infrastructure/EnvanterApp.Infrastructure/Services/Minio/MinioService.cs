using EnvanterApp.Application.Abstractions.Minio;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;

namespace EnvanterApp.Infrastructure.Services.Minio
{
    public class MinioService : IMinioService
    {
        private readonly MinioClient _minioClient;

        public MinioService(IConfiguration configuration, IMinioClient minioClient)
        {
            _minioClient = (MinioClient)minioClient;
        }

        public async Task<string> UploadFileAsync(string bucketName, IFormFile file)
        {
            bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

            if (!found)
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await _minioClient.PutObjectAsync(new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName)
                    .WithStreamData(stream)
                    .WithObjectSize(file.Length)
                    .WithContentType(file.ContentType));
            }
            return $"http://localhost:9000/{bucketName}/{fileName}";
        }

        public async Task<string> GetFileAsBase64Async(string bucketName, string fileName)
        {
            try
            {
                Uri uri = new Uri(fileName);
                string file = Path.GetFileName(uri.AbsolutePath);

                using (var ms = new MemoryStream())
                {
                     await _minioClient.GetObjectAsync(
                        new GetObjectArgs()
                            .WithBucket(bucketName)
                            .WithObject(file)
                            .WithCallbackStream(stream => stream.CopyTo(ms))
                    );
                    var bytes = ms.ToArray();
                    return Convert.ToBase64String(bytes);
                }
            }
            catch (Exception ex)
            {
                return ($"Dosya Bulunamadı! Hata Mesajı: {ex.Message}");
            }
        }
        public Task RemoveFileAsync(string bucketName, string fileUrl)
        {
            throw new NotImplementedException();
        }

       
    }
}
