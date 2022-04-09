using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T data)
        {
            IsSuccessed = true;
            Data = data;
        }

        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
       
    }
}
