using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Common.ViewModels
{
    public class ApiLogsVM
    {
        public int Id { get; set; }

        public string Api { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }
        public int MachineId { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }
        public Guid DeviceGuid { get; set; }
    }
}
