using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment5.Models
{
    public class SongGenrePerformerViewModel
    {

        public List<Song>? Songs { get; set; }
        public SelectList? Genres { get; set; }
        public SelectList? Performers { get; set; }
        public string? SongGenre { get; set; }
        public string? SongPerformer { get; set; }

    }
}
