using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities.FileUpload
{
    public interface IFileUploadService
    {
        Task<Response<FileUploadResult>> Upload(IFormFile file, string directory);
        Response Delete(string path);
    }
}
