using Microsoft.AspNetCore.Mvc;
using MVC_Data.Models.Data;
using MVC_Data.Models.Service;
using MVC_Data.Models.ViewModel;


namespace MVC_Data.Controllers
{
    public class PeopleController : Controller
    {
        IPeopleService _service;

        public PeopleController(IPeopleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ShowPersonEditButton"] = true;
            return View(_service.All());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //i.e the search filter was clicked
        public IActionResult Index(PeopleViewModel searchViewModel)
        {
            PeopleViewModel filteredModel = _service.FindBy(searchViewModel);
            searchViewModel.Persons = filteredModel.Persons;

            ModelState.Clear();
            return View(searchViewModel);
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
                EditPersonViewModel editPersonVM = new EditPersonViewModel(id, updPerson);
                return View(editPersonVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update(EditPersonViewModel person)
        {
            Person updPerson = _service.FindBy(person.Id);

            if (updPerson == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                Person updatedPerson = new Person();
                _service.Edit(person.Id, person);

                return RedirectToAction(nameof(Index));
            }

            //invalid modelstate
            EditPersonViewModel editPersonVM = new EditPersonViewModel(person.Id, updPerson);
            return View(editPersonVM);

        }
    }
}
