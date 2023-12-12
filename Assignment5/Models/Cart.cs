using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models
{
    public class Cart
    {

        public List<Song>? Songs { get; set; }
        [DataType(DataType.Currency)]
        public decimal total { get; set; }

    }
}
