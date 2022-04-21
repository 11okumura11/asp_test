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

namespace TestApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly asp_testContext _context;

        public MoviesController(asp_testContext context)
        {
            _context = context;
        }

        //GET: Movies
        // "パート 7、ASP.NET Core MVC アプリへの検索の追加"以降はこのメソッドをコメントアウトする
        //public IActionResult Index()
        //{
        //    return View(_context.Movies.ToList());
        //}

        //[HttpPost]
        //public string Index(string searchString, bool notUsed)
        //{
        //    return "From [HttpPost]Index: filter on " + searchString;
        //}

        // "パート 7、ASP.NET Core MVC アプリへの検索の追加"以降の、「ジャンルによる検索の追加」以前まではこのメソッドのコメントアウトを解除する
        // 「ジャンルによる検索の追加」以降はこのメソッドのコメントアウトする
        //public IActionResult Index(string searchString)
        //{
        //    var movies = from m in _context.Movies
        //                 select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        movies = movies.Where(s => s.Title!.Contains(searchString));
        //    }

        //    return View(movies.ToList());
        //}

        // "パート 7、ASP.NET Core MVC アプリへの検索の追加"の、「ジャンルによる検索の追加」以降はこのメソッドのコメントアウトを解除する
        public IActionResult Index(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Movies
                                            orderby m.Genre
                                            select m.Genre;
            var movies = from m in _context.Movies
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(genreQuery.Distinct().ToList()),
                Movies = movies.ToList()
            };

            return View(movieGenreVM);
        }

        //detailsのコメントurlからidを取得
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = from c in _context.Comments
                           where c.Movieid == id
                           select c;
            
            if (comments == null)
            {
                return NotFound();
            }
            return View(comments);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _context.Movies
                .FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
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

        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _context.Movies
                .FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.Find(id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
