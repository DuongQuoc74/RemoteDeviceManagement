using System;

namespace Jabil.Pico.Common.ViewModels
{
    public class TicketVM
    {
        public int Id { get; set; }

        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int ClassificationId { get; set; }
        public string ClassificationValue { get; set; }
        public DateTime? DateStop { get; set; }
        public DateTime? DateRun { get; set; }
        public DateTime? NewStartTime { get; set; }
        public int? Level { get; set; }
        public string PartNumber { get; set; }
        public string ClosedBy { get; set; }
        public string Description { get; set; }
    }
}