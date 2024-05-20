using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        //CRUD Operations

        //C
        //GET: Cinemas/Create/1
        public async Task<IActionResult> Create()
        {
            return View();
        }
        //POST: Cinemas/Create/
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            ModelState.Remove("Movies");
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        //R
        //GET: Cinemas/Index = Cinemas
        public async Task<IActionResult> Index()
        {
            var AllCinemas = await _service.GetAllAsync();
            return View(AllCinemas);
        }
        //GET: Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            //Check this actor id is exist in the DB or not
            var cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return View("NotFound");
            else
                return View(cinemaDetails);
        }

        //U
        //GET: Cinemas/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
                return View("NotFound");

            return View(cinemaDetails);
        }
        //POST: Cinemas/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            ModelState.Remove("Movies");
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.UpdateAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        //D
        //GET: Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null)
                return View("NotFound");

            return View(cinema);
        }
        //POST: Cinemas/Delete/1
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
