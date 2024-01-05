using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Common.ViewModels
{
    public class DeviceAddVM
    {
        public int MachineId { get; set; }
        public string Remark { get; set; }
        public MachineVM[] Machines { get; set; }
    }
}
