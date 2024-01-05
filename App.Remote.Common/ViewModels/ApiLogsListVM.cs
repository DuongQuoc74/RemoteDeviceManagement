using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Common.ViewModels
{
    public class ApiLogsListVM
    {
        public ApiLogsVM[] ApiLogs { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FindFromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FindToDate { get; set; }
        public string GuidDevice { get; set; }
    }
}
