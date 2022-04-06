using DaNangBayBooking.ViewModels.Catalog.Provinces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Provinces
{
    public interface IProvinceService 
    {
        Task<List<ProvinceVm>> GetAll();

        Task<ProvinceVm> GetById(Guid ProvinceID);
    }
}
