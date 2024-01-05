using System.ComponentModel.DataAnnotations.Schema;

namespace Jabil.Pico.Web.Models
{
    [Table("CT_MIDownTimeClassification")]
    public class Classification
    {
        [Column("PKClassificationID")]
        public int Id { get; set; }

        [Column("Classification")]
        public string Value { get; set; }
        public string Color { get; set; }
    }
}
