using Microsoft.AspNetCore.Mvc;
using asp_test.Models.Data;
using asp_test.Models;


namespace asp_test.Controllers
{
    public class CommentsUsersController : Controller
    {
        private readonly asp_testContext _context;

        public CommentsUsersController(asp_testContext context)
        {
            _context = context;
        }

        //detailsのコメントurlからidを取得
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CommentsUsers = from c in _context.Comments
                                join u in _context.Users
                                on c.Userid equals u.Id
                                where c.Movieid == id
                                select new CommentsUsersViewModels
                                {
                                    CommentId = c.Id,
                                    Name = u.Name,
                                    Gender = (u.Gender ? "男性" : "女性"),
                                    Comment1 = c.Comment1,
                                    CreatedAt = c.CreatedAt
                                };

            if (CommentsUsers == null)
            {
                return NotFound();
            }

            var CommentsUsersVM = new CommentsUsersViewModels
            {
                CommentsUsers = CommentsUsers.ToList()
            };

            return View(CommentsUsersVM);
        }

        // GET: Movies/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Users = from u in _context.Users
                        select new
                        {
                           Uid = u.Id,
                           Uname = u.Name
                        };

            if (Users == null)
            {
                return NotFound();
            }

            var CommentsUsersCM = new CommentsUsersCreateModels
            {
                Users = Users.ToList()
            };

            return View(CommentsUsersCM);
        }
        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Movieid, Userid, Comment1, CreatedAt ")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

    }
}