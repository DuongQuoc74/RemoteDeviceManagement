using System.ComponentModel.DataAnnotations.Schema;

namespace Jabil.Pico.Web.Models
{
    [Table("CT_Machines")]
    public class Machine
    {
        [Column("PKMachineID")]
        public int Id { get; set; }

        [Column("MachineName")]
        public string Name { get; set; }
        public bool Available { get; set; }

        [Column("FKLineID")]
        public int LineId { get; set; }
        public Line Line { get; set; }
    }
}
