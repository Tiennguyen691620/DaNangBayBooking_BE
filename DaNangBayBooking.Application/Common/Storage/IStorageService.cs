using DaNangBayBooking.ViewModels.Catalog.Images;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Common.Storage
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName);

        Task<ImageVm> SaveFileImgAsync(Stream mediaBinaryStream, string fileName);

        Task DeleteFileAsync(string fileName);


    }
}
