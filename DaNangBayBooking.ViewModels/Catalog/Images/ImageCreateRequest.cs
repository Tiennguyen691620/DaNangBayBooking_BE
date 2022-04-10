using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Images
{
    public class ImageCreateRequest
    {
        public IFormFile File { get; set; }
        public string? Watermark { get; set; }
    }
}
