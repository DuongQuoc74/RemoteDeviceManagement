using System.ComponentModel.DataAnnotations.Schema;

namespace Jabil.Pico.Web.Models
{
    [Table("CT_UsersRols")]
    public class UserRole
    {
        [Column("PKRegisterID")]
        public int Id { get; set; }

        public string EmployeeNumber { get; set; }

        public string WindowsNT { get; set; }

        [Column("FKRolID")]
        public int RoleId { get; set; }

        public bool Available { get; set; }
    }
}