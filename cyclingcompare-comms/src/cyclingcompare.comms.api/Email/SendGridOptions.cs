using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cyclingcompare.comms.api.Email
{
    public class SendGridOptions
    {
        public string ApiKey { get; set; }
        public string ContactUsEmail { get; set; }
    }
}
