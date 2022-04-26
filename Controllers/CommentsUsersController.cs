#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                CommentsUsers = CommentsUsers.ToList(),
                MovieId = id
            };

            return View(CommentsUsersVM);
        }

        // GET: Movies/Create
        public IActionResult Create(int? id)
        {
            IEnumerable<User> users = _context.Users;
            if (users == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> items = users.Select(u => 
            new SelectListItem() { Value = u.Id.ToString(), Text = u.Name});

            var CommentsUsersVM = new CommentsUsersViewModels(){ UsersList = items };

            if (CommentsUsersVM == null)
            {
                return NotFound();
            }

            return View(CommentsUsersVM);
        }
        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id ,[Bind("Userid, Comment1")] CommentsUsersViewModels CommentsUsersVM)
        {
            if (ModelState.IsValid)
            {
                var query = from c in _context.Comments
                            select c.Id;

                _context.Add(new Comment
                {
                    Id = query.Max() + 1,
                    Movieid = id,
                    Userid = CommentsUsersVM.Userid,                
                    Comment1 = CommentsUsersVM.Comment1,
                    CreatedAt = DateTime.Now
                });

                // SaveChangesが呼び出された段階で初めてInsert文が発行される
                _context.SaveChanges();
                return RedirectToAction("Index",new { Id = id });
            }
            return View(CommentsUsersVM);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Users = from u in _context.Users
                        select u;

            if (Users == null)
            {
                return NotFound();
            }

            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                return NotFound();
            }

            var CommentsUsersVM = new CommentsUsersViewModels
            {
                Users = Users.ToList(),
                CommentId = comment.Id,
                Comment1 = comment.Comment1,
                Userid = comment.Userid
            };

            return View(CommentsUsersVM);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }


    }
}