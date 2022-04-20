using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace asp_test.Models
{
    public class MovieGenreViewModel
    {
        public List<Data.Movie>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
    }
}