﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace asp_test.Models
    {
        public class CommentsUsersCreateModels
        {
            public List<Data.User>? Users { get; set; }
            public int Id { get; set; }
            public int Movieid { get; set; }
            public int Userid { get; set; }
            public string Comment1 { get; set; } = null!;
            public DateTime CreatedAt { get; set; }


    }
}