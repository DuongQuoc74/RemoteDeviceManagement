using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jabil.Pico.Web.Models
{
    [Table("PI_ApiLogs")]
    public class ApiLog
    {
        [Column("PKApiLogsID")]
        public int Id { get; set; }

        public string Api { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }
        [Column("FKMachineId")]
        public int MachineId { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }
        public Guid DeviceGuid { get; set; }
    }
}