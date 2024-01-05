using System.ComponentModel.DataAnnotations.Schema;

namespace Jabil.Pico.Web.Models
{
    [Table("PI_Settings")]
    public class Setting
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public bool Available { get; set; }
        public bool IsPico { get; set; }
    }
}