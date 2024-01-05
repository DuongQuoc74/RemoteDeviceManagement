using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Common.ViewModels
{
    public class MachineVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }
        public int DeviceId { get; set; }
        public Guid? DeviceGuid { get; set; }
        public bool? DeviceAvailable { get; set; }
        public string DeviceStatus { get; set; }
    }
}
