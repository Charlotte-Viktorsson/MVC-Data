using Microsoft.AspNetCore.Mvc;
using MVC_Data.Models.Data;
using MVC_Data.Models.Service;
using MVC_Data.Models.ViewModel;


namespace MVC_Data.Controllers
{
    public class PeopleController : Controller
    {
        IPeopleService _service;
        private readonly ICityService _cityService;
        private readonly ILanguageService _languageService;
        private readonly IPersonLanguageService _personLanguageService;

        public PeopleController(IPeopleService service, ICityService cityService, ILanguageService languageService, IPersonLanguageService personLanguageService)
        {
            _service = service;
            _cityService = cityService;
            _languageService = languageService;
            _personLanguageService = personLanguageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ShowPersonEditButton"] = true;
            PeopleViewModel peopleViewModel = _service.All();
            peopleViewModel.createPersonViewModel.Cities = _cityService.All().Cities;
            return View(peopleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //i.e the search filter was clicked
        public IActionResult Index(PeopleViewModel searchViewModel)
        {
            PeopleViewModel filteredModel = _service.FindBy(searchViewModel);
            searchViewModel.Persons = filteredModel.Persons;

            ModelState.Clear();
            ViewData["ShowPersonEditButton"] = true;
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
            createPerson.createPersonViewModel.Cities = _cityService.All().Cities;
            return View("Index", createPerson);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Person updPerson = _service.FindBy(id);
            if (updPerson != null)
            {
                EditPersonViewModel editPersonVM = new EditPersonViewModel(id, updPerson);
                editPersonVM.CreatePerson.Cities = _cityService.All().Cities;
                return View(editPersonVM);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update(EditPersonViewModel personVM)
        {
            Person updPerson = _service.FindBy(personVM.Id);

            if (updPerson == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                _service.Edit(personVM.Id, personVM);

                return RedirectToAction(nameof(Index));
            }

            return View(personVM);
        }

        [HttpGet]
        public IActionResult UpdatePersonLanguages(int id)
        {
            Person updPerson = _service.FindBy(id);
            if (updPerson != null)
            {
                ManagePersonLanguageViewModel vm = new ManagePersonLanguageViewModel(updPerson);
                vm.NotSpokenLanguages = _service.GetNotSpokenLanguages(id);
                return View(vm);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult AddLanguageToPerson(int pId, int langId)
        {
            Person person = _service.FindBy(pId);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            PersonLanguage pl = _personLanguageService.Create(
                new PersonLanguage() { PersonId = pId, LanguageId = langId }
            );


            return RedirectToAction("UpdatePersonLanguages", new { id = pId });
        }

        [HttpGet]
        public IActionResult RemoveLanguageFromPerson(int pId, int langId)
        {
            Person person = _service.FindBy(pId);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            _personLanguageService.Remove(pId, langId);

            return RedirectToAction("UpdatePersonLanguages", new { id = pId });
        }
    }
}
