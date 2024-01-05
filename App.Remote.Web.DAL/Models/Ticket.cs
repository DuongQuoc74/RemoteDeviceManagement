using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jabil.Pico.Web.Models
{
    [Table("CT_Tikets")]
    public class Ticket
    {
        [Column("PKTiketID")]
        public int Id { get; set; }

        [Column("FKMachineID")]
        public int MachineId { get; set; }
        public virtual Machine Machine { get; set; }

        [Column("FKClasificationID")]
        public int ClassificationId { get; set; }
        public virtual Classification Classification { get; set; }

        public DateTime DateStop { get; set; }
        public DateTime? DateRun { get; set; }
        public DateTime? NewStartTime { get; set; }
        public int? Level { get; set; }
        public string PartNumber { get; set; }
        public string ClosedBy { get; set; }
        public string Description { get; set; }
    }
}
