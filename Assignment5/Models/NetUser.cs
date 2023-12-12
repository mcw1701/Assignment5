using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models
{
    public class NetUser
    {

        public required int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public required string UserName { get; set; }

        [StringLength(60, MinimumLength = 6)]
        [RegularExpression(@"[0-9A-Za-z_$%#@!?&]*")]
        [DataType(DataType.Password)]
        public required string UserPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[0-9A-Za-z]*@[a-z]*.[a-z]*")]
        public required string UserEmail { get; set; }
    }
}
