using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_test.Models.Data
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; } = null!;

        public double Price { get; set; }
    }
}
