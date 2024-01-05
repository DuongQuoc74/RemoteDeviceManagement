using Jabil.Pico.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jabil.Pico.Web.DAL.Models
{
    [Table("PI_Devices")]
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PKPicoID")]
        public int Id { get; set; }
        [Column("FKMachineID")]
        public int MachineId { get; set; }
        public virtual Machine Machine { get; set; }
        public Guid Guid { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public bool Available { get; set; }
    }
}
