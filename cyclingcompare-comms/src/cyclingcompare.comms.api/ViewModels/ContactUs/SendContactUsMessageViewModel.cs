using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cyclingcompare.comms.api.ViewModels.ContactUs
{
    public class SendContactUsMessageViewModel
    {
        public string FromEmail { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
