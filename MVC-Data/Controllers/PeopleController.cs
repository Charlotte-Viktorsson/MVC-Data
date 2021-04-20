using Microsoft.AspNetCore.Mvc;
using MVC_Data.Models.Data;
using MVC_Data.Models.Service;
using MVC_Data.Models.ViewModel;


namespace MVC_Data.Controllers
{
    public class PeopleController : Controller
    {
        private PeopleService _service = new PeopleService();
        private PeopleViewModel _peopleViewModel = new PeopleViewModel();

        public PeopleController()
        {
            _peopleViewModel = _service.All();
        }

        public IActionResult Index()
        {
            return View(_peopleViewModel);

        }

        public IActionResult Remove(int id)
        {

            if (_service.Remove(id))
            {
                _peopleViewModel = _service.All();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //hm, show some error ? can trigger this by manually
                //changing url to ../People/Remove/55 or similar
                return View("Index", _peopleViewModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PeopleViewModel createPerson)
        {
            if (ModelState.IsValid)
            {
                _service.Add(createPerson.createPersonViewModel);
                _peopleViewModel = _service.All();
                return RedirectToAction(nameof(Index));
            }

            _peopleViewModel = _service.All();
            return View("Index", _peopleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(PeopleViewModel searchViewModel)
        {
            string filter = searchViewModel.SearchFilter;

            _peopleViewModel = _service.FindBy(searchViewModel);

            //return RedirectToAction(nameof(Index)); //verkar tömma _peopleViewModel
            return View("Index", _peopleViewModel); //denna verkar sortera rätt,
                                                    //men visar felmedd från create-delen
        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            Person updPerson = _service.FindBy(id);
            if (updPerson != null)
            {
                //don't have a view for it yet, just hardcoding some updates for testing
                updPerson.LastName = updPerson.LastName + " Updated";
                updPerson.FirstName = updPerson.FirstName + " Updated";
                updPerson.City = updPerson.City + " Updated";

                updPerson = _service.Edit(id, updPerson);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
