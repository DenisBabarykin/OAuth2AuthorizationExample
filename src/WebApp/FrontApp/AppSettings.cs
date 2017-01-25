using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace FrontApp
{
    public class AppSettings
    {
        public string BookingServerUri { get; set; }

        public string BillingServerUri { get; set; }

        public string RoomServerUri { get; set; }
    }
}
