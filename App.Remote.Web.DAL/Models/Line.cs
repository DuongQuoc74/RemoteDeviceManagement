using System.ComponentModel.DataAnnotations.Schema;

namespace Jabil.Pico.Web.Models
{
    [Table("CT_Lines")]
    public class Line
    {
        [Column("PKLineID")]
        public int Id { get; set; }

        [Column("LineName")]
        public string Name { get; set; }

        [Column("FKBuildingID")]
        public int BuildingId { get; set; }
    }
}
