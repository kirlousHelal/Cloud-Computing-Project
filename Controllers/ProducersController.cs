using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }

        //CRUD Operations

        //C
        //GET: Producers/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        //POST: Producers/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            ModelState.Remove("Movies");
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //R
        //GET: Producers/Index = Producers
        public async Task<IActionResult> Index()
        {
            var AllProducers = await _service.GetAllAsync();
            return View(AllProducers);
        }
        //GET: Producers/Details/1
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
        //GET: Producers/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return View("NotFound");

            return View(actorDetails);
        }
        //POST: Producers/Edit/1
        //in here we bind the attributes becuase we won't set all the attribute in actor model (we set only the Bio, FullName, ProfilePictureURL)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            ModelState.Remove("Movies");
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.UpdateAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //D
        //GET: Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
                return View("NotFound");

            return View(producerDetails);
        }
        //POST: Actors/Delete/1
        //in here we bind the attributes becuase we won't set all the attribute in actor model (we set only the Bio, FullName, ProfilePictureURL)
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
                return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
