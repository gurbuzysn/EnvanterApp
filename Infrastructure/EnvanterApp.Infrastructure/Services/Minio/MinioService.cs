using EnvanterApp.Application.Abstractions.Minio;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;

namespace EnvanterApp.Infrastructure.Services.Minio
{
    public class MinioService : IMinioService
    {
        private readonly MinioClient _minioClient;

        public MinioService(MinioClient minioClient)
        {
            _minioClient = minioClient;
        }
     
        public async Task<string> UploadFileAsync(IFormFile file, string bucketName)
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
    }
}
