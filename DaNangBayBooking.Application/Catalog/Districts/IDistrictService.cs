using DaNangBayBooking.ViewModels.Catalog.Districts;
using DaNangBayBooking.ViewModels.Catalog.Provinces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Districts
{
    public interface IDistrictService
    {
        Task<List<DistrictVm>> GetAll(Guid ProvinceID);

        //Task<DistrictVm> GetById(Guid ProvinceID);
    }
}
