﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Abstractions.Minio
{
    public interface IMinioService
    {
        Task<string> UploadFileAsync(string bucketName, IFormFile file);
        Task RemoveFileAsync(string bucketName, string fileUrl);
    }
}
