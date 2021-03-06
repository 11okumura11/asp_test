using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace asp_test.Models
{
    public class CommentsUsersViewModels
    {
        public List<CommentsUsersViewModels>? CommentsUsers { get; set; }
        public int? MovieId { get; set; }
        public int? CommentId { get; set; }
        public string? Name { get; set; }
        public String? Gender { get; set; }
        
        public String? Comment1 { get; set; }
        public DateTime? CreatedAt { get; set; }

        public List<Data.User>? Users { get; set; }
        public IEnumerable<SelectListItem> UsersList { get; set; }
        public int Userid { get; set; }
    }
}