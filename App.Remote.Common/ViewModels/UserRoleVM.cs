using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabil.Pico.Common.ViewModels
{
    public class UserRoleVM
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string WindowsNT { get; set; }
        public int RoleId { get; set; }
        public bool Available { get; set; }
    }
}
