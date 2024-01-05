using System;
using System.ComponentModel.DataAnnotations;

namespace Jabil.Pico.Common.ViewModels
{
    public class TicketListVM
    {
        public TicketVM[] Tickets { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FindFromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FindToDate { get; set; }
        public string FindMachineName { get; set; }

    }
}