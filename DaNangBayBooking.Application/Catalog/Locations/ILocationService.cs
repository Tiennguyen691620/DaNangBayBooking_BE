using DaNangBayBooking.ViewModels.Catalog.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.Wards
{
    public interface ILocationService
    {
        Task<List<LocationVm>> GetAll(Guid DistrictID);
    }
}
