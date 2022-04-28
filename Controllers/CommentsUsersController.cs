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

            CommentsUsersViewModels CommentsUsersVM = new CommentsUsersViewModels();
            ViewBag.UsersList = new SelectList(_context.Users, "Id", "Name");
            CommentsUsersVM.UsersList = ViewBag.UsersList;

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
        public IActionResult Create(int id, String UsersList, [Bind("UsersList, Comment1")] CommentsUsersViewModels CommentsUsersVM)
        {
            if (ModelState.IsValid)
            {
                var query = from c in _context.Comments
                            select c.Id;

                 int Userid = int.Parse(UsersList);

                _context.Add(new Comment
                {
                    Id = query.Max() + 1,
                    Movieid = id,
                    Userid = Userid,                
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

            var users = from u in _context.Users
                        select u;

            if (users == null)
            {
                return NotFound();
            }

            Comment comment = _context.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            CommentsUsersViewModels CommentsUsersVM = new CommentsUsersViewModels();
                ViewBag.UsersList = new SelectList(_context.Users, "Id", "Name", comment.Userid);
                CommentsUsersVM.UsersList = ViewBag.UsersList;
                CommentsUsersVM.Comment1 = comment.Comment1;

            return View(CommentsUsersVM);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, String UsersList, [Bind("UsersList, Comment1")] CommentsUsersViewModels CommentsUsersVM)
        {
            if (ModelState.IsValid)
            {
                Comment comment = _context.Comments.Find(id);

                comment.Userid = int.Parse(UsersList);
                comment.Comment1 = CommentsUsersVM.Comment1;
                comment.UpdatedAt = DateTime.Now;

                _context.Comments.Update(comment);
      
                // SaveChangesが呼び出された段階で初めてInsert文が発行される
                _context.SaveChanges();

                return RedirectToAction("Index", new { Id = comment.Movieid });
            }
            return View(CommentsUsersVM);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                return NotFound();
            }

            var user = _context.Users.Find(comment.Userid);

            if (user == null)
            {
                return NotFound();
            }

            CommentsUsersViewModels CommentsUsersVM = new CommentsUsersViewModels()
            {
                MovieId = comment.Movieid,
                Name = user.Name,
                Gender = ( user.Gender? "男性" : "女性"),
                Comment1 = comment.Comment1,
                CreatedAt = comment.CreatedAt
            };

            if (CommentsUsersVM == null)
            {
                return NotFound();
            }


            return View(CommentsUsersVM);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var comment = _context.Comments.Find(id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Index", new { Id = comment.Movieid });
        }
    }
}