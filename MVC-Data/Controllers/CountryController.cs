using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Data.Models.Data;
using MVC_Data.Models.Service;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public CountryController(ICountryService countryService, ICityService cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
        }

        // GET: CountryController
        public ActionResult Index()
        {
            CountryViewModel countryVM = new CountryViewModel();
            countryVM = _countryService.All();
            return View(countryVM);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            Country editCountry = _countryService.FindBy(id);
            if (editCountry == null)
            {
                return RedirectToAction(nameof(Index));
            }

            EditCountryViewModel editCountryViewModel = new EditCountryViewModel(id, editCountry);
            editCountryViewModel.CreateVM.CityList = _cityService.FindCitiesByNation(id);
            return View(editCountryViewModel);
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            CreateCountryViewModel createCountry = new CreateCountryViewModel();
            return View(createCountry);
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCountryViewModel createCountry)
        {
            if (ModelState.IsValid)
            {
                _countryService.Add(createCountry);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(createCountry);
            }

        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            Country editCountry = _countryService.FindBy(id);
            if (editCountry == null)
            {
                return RedirectToAction(nameof(Index));
            }

            EditCountryViewModel editCountryViewModel = new EditCountryViewModel(id, editCountry);

            return View(editCountryViewModel);
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditCountryViewModel editCountryViewModel)
        {
            Country updCountry = _countryService.FindBy(id);
            if (updCountry == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                _countryService.Edit(id, editCountryViewModel);
                return RedirectToAction(nameof(Index));
            }
            //invalid modelstate
            return View(editCountryViewModel);
        }

        // GET: CountryController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Country countryToDelete = _countryService.FindBy(id);
            countryToDelete.Cities = _cityService.FindCitiesByNation(id);
            return View(countryToDelete);
        }

        // POST: CountryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            bool result = _countryService.Remove(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            //lets go back to index anyhow
            return RedirectToAction(nameof(Index));
        }
    }
}
