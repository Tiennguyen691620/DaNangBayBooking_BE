using DaNangBayBooking.Utilities.Constants;
using DaNangBayBooking.ViewModels.Catalog.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
        private readonly string _filePath;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;


        private const string IMG_CONTENT_FOLDER_NAME = "Images";
        private const string ACCOMMODATION_CONTENT_FOLDER_NAME = "Accommodation-content";
        //private const string filePath = new Uri(_configuration["BaseAddress"]);


        public FileStorageService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _configuration = configuration;
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, Path.Combine(IMG_CONTENT_FOLDER_NAME, ACCOMMODATION_CONTENT_FOLDER_NAME));
            _webHostEnvironment = webHostEnvironment;
            //_filePath = Path.Combine(, Path.Combine(IMG_CONTENT_FOLDER_NAME, ACCOMMODATION_CONTENT_FOLDER_NAME));

        }

        public string GetFileUrl(string fileName)
        {
            return $"/{IMG_CONTENT_FOLDER_NAME}/{ACCOMMODATION_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task<ImageVm> SaveFileImgAsync(Stream mediaBinaryStream, string fileName)
        {
            //var fileUrl = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
            return new ImageVm()
            {
                FileUrl = Path.Combine(Path.Combine(_configuration["BaseAddress"], Path.Combine(IMG_CONTENT_FOLDER_NAME, ACCOMMODATION_CONTENT_FOLDER_NAME)), fileName),
                Container = ACCOMMODATION_CONTENT_FOLDER_NAME
            };
        }

        public async Task DeleteFileAsync(string fileName)
        {
            fileName = fileName.Replace(_configuration["BaseAddress"], _webHostEnvironment.WebRootPath);
            var filePath = fileName;
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
