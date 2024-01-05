using Jabil.Pico.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Web.DAL.Models
{
    [Table("PI_DeviceHistories")]
    public class DeviceHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PKDeviceHistoryID")]
        public int Id { get; set; }
        [Column("FKPicoID")]
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }

        [Column("FKMachineID")]
        public int MachineId { get; set; }
        public virtual Machine Machine { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Remark { get; set; }
    }
}
