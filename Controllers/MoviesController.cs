using Azure;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMoviesService _service;
        public MoviesController(ApplicationDbContext context, IMoviesService service)
        {
            _service = service;
            _context = context;
        }

        //CRUD Operations

        //C
        //GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropDownnsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropDownnsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownnsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownnsData.Actors, "Id", "FullName");

            return View();
        }
        //POST: Movies/Create
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropDownnsData = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropDownnsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropDownnsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropDownnsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //R
        public IActionResult Index()
        {
            var AllMovies = _context.Movies.Include(m => m.Cinema).ToList();
            return View(AllMovies);
        }
        //GET: Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if (movie == null)
                return View("NotFound");
            return View(movie);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            if (searchString != null)
            {
                var movie = allMovies.Where(m => m.Name == searchString).ToList();
                return View(nameof(Index), movie);
            }
            return View(nameof(Index), allMovies);
        }

        //U
        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null)
            {
                return View("NotFound");
            }

            var respone = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList()
            };

            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
            return View(respone);
        }
        //POST: Movies/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) 
                return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //D
        //GET: Delete/Edit/1
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if (movie == null)
                return View("NotFound");
            return View(movie);
        }
        //POST: Delete/Edit/1
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
