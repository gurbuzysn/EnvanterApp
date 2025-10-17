using EnvanterApp.Application.Abstractions.Minio;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Infrastructure.Services.Minio
{
    public class MinioService : IMinioService
    {
        private readonly MinioClient _minioClient;

        public MinioService(IConfiguration configuration)
        {
            var endpoint = configuration["Minio:Endpoint"];
            var accessKey = configuration["Minio:AccessKey"];
            var secretKey = configuration["Minio:SecretKey"];
            var useSSL = bool.Parse(configuration["Minio:UseSSL"] ?? "false");

            _minioClient.WithEndpoint(endpoint)
                        .WithCredentials(accessKey, secretKey)
                        .WithSSL(useSSL)
                        .Build();



        }



        public async Task<string> UploadFileAsync(string bucketName, IFormFile file)
        {
            //bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));


            //            if (!found)
            //                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));

            //            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            //            using (var stream = file.OpenReadStream())
            //            {
            //                await _minioClient.PutObjectAsync(new PutObjectArgs()
            //                    .WithBucket(bucketName)
            //                    .WithObject(fileName)
            //                    .WithStreamData(stream)
            //                    .WithObjectSize(file.Length)
            //                    .WithContentType(file.ContentType));
            //            }

            //            return $"http://localhost:9000/{bucketName}/{fileName}";

            var aaa = "AbidikGubidik";

            return aaa;

        }




        public Task RemoveFileAsync(string bucketName, string fileUrl)
        {
            throw new NotImplementedException();
        }

    }
}
