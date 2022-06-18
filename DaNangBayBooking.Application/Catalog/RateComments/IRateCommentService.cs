using DaNangBayBooking.ViewModels.Catalog.RateComment;
using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaNangBayBooking.Application.Catalog.RateComments
{
    public interface IRateCommentService
    {
        Task<ApiResult<bool>> CreateRateComment(CreateRateCommentRequest request);

        Task<ApiResult<List<RateCommentVm>>> GetAllRateComment(GetAllRateCommentRequest request);

        Task<ApiResult<GetQtyRateComment>> GetQtyAndPontRateComment(Guid AccommodationId);
    }
}
