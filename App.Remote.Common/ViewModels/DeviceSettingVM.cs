using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Common.ViewModels
{
    public class DeviceSettingVM
    {
        public string OnlineTimeOutMinutes { get; set; }
        public string TicketsApiCheckTimeOutMinutes { get; set; }
        public string TicketsValidDays { get; set; }
        public string CloseTicketURL { get; set; }
        public string CloseTicketParameterProcess { get; set; }
        public string CloseTicketParameterProcesId { get; set; }
    }
}
