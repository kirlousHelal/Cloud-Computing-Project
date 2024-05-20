using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        //Service interface Injection (using constructor injection method)
        private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        //CRUD Operations
        //C
        //GET: Actors/Create
        //mmken ashil l async becuase there is no data manipulation
        public IActionResult Create()
        {
            return View();
        }
        //in here we bind the attributes becuase we won't set all the attribute in actor model (we set only the Bio, FullName, ProfilePictureURL)
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            ModelState.Remove("Actors_Movies");
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //R
        //GET: Actors/Index = Actors
        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAllAsync();
            return View(actors);
        }
        //GET: Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            //Check this actor id is exist in the DB or not
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return View("NotFound");
            else
                return View(actorDetails);
        }

        //U
        //GET: Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return View("NotFound");

            return View(actorDetails);

        }
        //POST: Actors/Edit/1
        //in here we bind the attributes becuase we won't set all the attribute in actor model (we set only the Bio, FullName, ProfilePictureURL)
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Actor actor)
        {
            ModelState.Remove("Actors_Movies");
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //D
        //GET: Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return View("NotFound");

            return View(actorDetails);
        }
        //POST: Actors/Delete/1
        //in here we bind the attributes becuase we won't set all the attribute in actor model (we set only the Bio, FullName, ProfilePictureURL)
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
