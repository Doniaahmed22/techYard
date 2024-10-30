using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techYard.Service.Services.FileHandlingService
{
    public interface IFileHandling
    {
        Task<string> SaveFileAsync(IFormFile file, string folderPath = "uploads");
        Task<bool> DeleteFileAsync(string filePath);
        string GetFileUrl(string filePath);
    }
}
