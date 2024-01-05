using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Common.ViewModels
{
    public class DeviceHistoryVM
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int MachineId { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string Remark { get; set; }
    }
}
