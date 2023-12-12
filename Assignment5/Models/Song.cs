using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment5.Models
{
    public class Song
    {

        public required int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public required string Title { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-z\s]*$")]
        [StringLength(60, MinimumLength = 3)]
        public required string Genre { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public required string Performer { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public required decimal Price { get; set; }

    }
}
