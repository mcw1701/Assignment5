using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment5.Models
{
    public class Songs
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }



    }
}
