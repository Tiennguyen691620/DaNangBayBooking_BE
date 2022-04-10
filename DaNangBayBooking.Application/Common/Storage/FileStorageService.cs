using DaNangBayBooking.ViewModels.Catalog.Images;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Common.Storage
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;

        private const string IMG_CONTENT_FOLDER_NAME = "Images";
        private const string ACCOMMODATION_CONTENT_FOLDER_NAME = "Accommodation-content";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, Path.Combine(IMG_CONTENT_FOLDER_NAME, ACCOMMODATION_CONTENT_FOLDER_NAME));


        }

        public string GetFileUrl(string fileName)
        {
            return $"/{IMG_CONTENT_FOLDER_NAME}/{ACCOMMODATION_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task<ImageVm> SaveFileImgAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
            return new ImageVm()
            {
                FileUrl = filePath,
                Container = ACCOMMODATION_CONTENT_FOLDER_NAME
            };
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
