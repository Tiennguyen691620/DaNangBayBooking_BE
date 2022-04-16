using DaNangBayBooking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.System.Users
{
    public class GetUserPagingRequest :  PagingRequestBase
    {
        public string SearchKey { get; set; }

        public Guid? RoleID { get; set; }
    }
}
