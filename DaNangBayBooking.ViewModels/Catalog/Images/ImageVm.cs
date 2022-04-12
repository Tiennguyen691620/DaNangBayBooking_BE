using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Catalog.Images
{
    public class ImageVm
    {
        public Guid ImageID { get; set; }
        public string FileName { get; set; }
        public string OrgFileName { get; set; }
        public string OrgFileExtension { get; set; }
        public string FileUrl { get; set; }
        public string Container { get; set; }
    }
}
