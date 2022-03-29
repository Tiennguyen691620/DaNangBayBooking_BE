using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        private string v;

        public ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }

        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }

        public ApiSuccessResult(string v)
        {
            this.v = v;
        }
    }
}
