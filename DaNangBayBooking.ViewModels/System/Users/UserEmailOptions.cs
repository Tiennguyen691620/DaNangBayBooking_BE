﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DaNangBayBooking.ViewModels.System.Users
{
    public class UserEmailOptions
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<KeyValuePair<string, string>> PlaceHolders { get; set; }
    }
}
