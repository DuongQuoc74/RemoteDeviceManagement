using System;

namespace Jabil.Pico.Common.ViewModels
{
    public class DeviceLogVM
    {
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string PreviousStatus { get; set; }
        public string Remark { get; set; }
    }
}
