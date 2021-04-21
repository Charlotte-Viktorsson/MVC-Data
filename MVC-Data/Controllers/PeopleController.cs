using Microsoft.AspNetCore.Mvc;
using MVC_Data.Models.Data;
using MVC_Data.Models.Service;
using MVC_Data.Models.ViewModel;


namespace MVC_Data.Controllers
{
    public class PeopleController : Controller
    {
        private IPeopleService _service = new PeopleService();

        [HttpGet]
        public IActionResult Index()
        {
            return View(_service.All());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //i.e the search filter was clicked
        public IActionResult Index(PeopleViewModel searchViewModel)
        {
            PeopleViewModel filteredModel = _service.FindBy(searchViewModel);
            searchViewModel.Persons = filteredModel.Persons;
            ModelState.Clear();
            return View(searchViewModel);   //visar en massa felmedd på create-delen av vyn
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            if (_service.Remove(id))
            {
                return RedirectToAction(nameof(Index));
            }
            else //couldn't remove but go to same place anyway for now
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PeopleViewModel createPerson)
        {
            if (ModelState.IsValid)
            {
                _service.Add(createPerson.createPersonViewModel);

                return RedirectToAction(nameof(Index));
            }
            createPerson = _service.All();
            return View("Index", createPerson);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Person updPerson = _service.FindBy(id);
            if (updPerson != null)
            {
                //don't have a view for it yet, just hardcoding an update for testing
                updPerson.LastName = updPerson.LastName + " Updated";

                updPerson = _service.Edit(id, updPerson);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
