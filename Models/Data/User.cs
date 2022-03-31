using System;
using System.Collections.Generic;

namespace asp_test.Models.Data
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Gender { get; set; }
    }
}
