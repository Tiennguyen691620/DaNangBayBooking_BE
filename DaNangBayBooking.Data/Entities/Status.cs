using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.Data.Entities
{
    public class Status
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string DisplayText { get; set; }

        public string Group { get; set; }

        public int? Order { get; set; }
    }
}
