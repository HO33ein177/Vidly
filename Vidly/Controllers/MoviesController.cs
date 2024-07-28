using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Vidly.Models;
using Vidly.ViewModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.Ajax.Utilities;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Movies
        public ActionResult MovieView()
        {
            var movie = new Movie()
            {
                Name = "Shrek!!!!!"
            };

            var customers = new List<Customer>
            {
                new Customer {Name = "Hossein" },
                new Customer {Name = "Ali" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new NewMovieViewModel(movie)
            {
                Genre = _context.Genre.ToList()
            };

            return View("NewMovieForm", viewModel);

        }



        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m=> m.Genre).ToList();
            
            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewMovieViewModel(movie)
                {
                    Genre = _context.Genre.ToList()
                };
                return View("NewMovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {

                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.Genre = movie.Genre;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;

            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Movies");
        }



        [Route("movies/released/{year}/{month:regex(\\d{2})}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult New()
        {
            var genre = _context.Genre.ToList();
            var movie = new Movie();
            var viewModel = new NewMovieViewModel(movie)
                {
                    Genre = genre
                };
            return View("NewMovieForm", viewModel);
            
        }
    }
}