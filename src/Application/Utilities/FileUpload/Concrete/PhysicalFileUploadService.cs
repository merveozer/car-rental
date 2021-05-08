using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities.FileUpload.Concrete
{
    class PhysicalFileUploadService : IFileUploadService
    {
        private const string ROOT = "wwwroot/";
        public async Task<Response<FileUploadResult>> Upload(IFormFile file, string directory)
        {
            if (file == null || file.Length == 0)
                return Response<FileUploadResult>.Fail("Lütfen dosyayı seçiniz.");


            string extension = file.FileName.Substring(file.FileName.LastIndexOf("."));
            string fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
            string path = Path.Combine(Directory.GetCurrentDirectory(), ROOT, directory, fileName);
        
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Response<FileUploadResult>.Success("", new FileUploadResult
            {
                Url = $"{directory}{fileName}"
            });
                
        }
        public Response Delete(string path)
        {
            path = ROOT + path;
            if (File.Exists(path))
            {
                File.Delete(path);
                return Response.Success();
            }
            else
            {
                return Response.Fail("Dosya bulunamadı.");
            }
        }

    }
}
