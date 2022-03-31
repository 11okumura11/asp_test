using System;
using System.Collections.Generic;

namespace asp_test.Models.Data
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int Movieid { get; set; }
        public int Userid { get; set; }
        public string Comment1 { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
