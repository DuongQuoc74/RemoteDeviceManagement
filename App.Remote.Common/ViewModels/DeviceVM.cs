using System;

namespace Jabil.Pico.Common.ViewModels
{
    public class DeviceVM
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int? LineId { get; set; }
        public string LineName { get; set; }
        public DateTime? LastOnlineCheck { get; set; }
        public Guid Guid { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? TicketDateStop { get; set; }
        public string TicketPartNumber { get; set; }
        public string TicketClosedBy { get; set; }
        public int? TicketId { get; set; }
        public string Api { get; set; }
        public DateTime? ApiCreated { get; set; }
        public string ApiCreatedBy { get; set; }
        public TicketListVM TicketListVM { get; set; }
        public DeviceHistoryVM[] ListDeviceHistoryVM { get; set; }
    }
}