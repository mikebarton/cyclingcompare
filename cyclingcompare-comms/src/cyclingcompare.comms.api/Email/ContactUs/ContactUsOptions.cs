﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cyclingcompare.comms.api.Email.ContactUs
{
    public class ContactUsOptions
    {
        public string TargetEmail { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
    }
}
