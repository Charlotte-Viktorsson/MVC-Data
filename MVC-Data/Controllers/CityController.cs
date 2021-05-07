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
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly IPeopleService _peopleService;

        public CityController(ICityService cityService, ICountryService countryService, IPeopleService peopleService)
        {
            _cityService = cityService;
            _countryService = countryService;
            _peopleService = peopleService;
        }

        // GET: CityController
        public ActionResult Index()
        {
            CityViewModel cityVM = new CityViewModel();
            cityVM = _cityService.All();

            return View(cityVM);
        }

        // GET: CityController/Details/5
        public ActionResult Details(int id)
        {
            City city = _cityService.FindBy(id);

            if (city != null)
            {
                CityDetailsViewModel cityDetailsVM = new CityDetailsViewModel();
                cityDetailsVM.Id = id;
                cityDetailsVM.Citizens = _peopleService.FindByCity(id);
                cityDetailsVM.CityName = city.Name;
                cityDetailsVM.NationName = _countryService.FindBy(city.Nation.Id).Name;
                return View(cityDetailsVM);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CityController/Create
        public ActionResult Create()
        {
            CreateCityViewModel createVM = new CreateCityViewModel();
            createVM.Countries = _countryService.All().Countries;
            return View(createVM);
        }

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCityViewModel createCity)
        {
            if (ModelState.IsValid)
            {
                City addedCity = _cityService.Add(createCity);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(createCity);
            }
        }

        // GET: CityController/Edit/5
        public ActionResult Edit(int id)
        {
            City city = _cityService.FindBy(id);
            if (city != null)
            {
                CreateCityViewModel createModel = new CreateCityViewModel();
                createModel.Name = city.Name;
                createModel.NationId = city.Nation.Id;
                createModel.Countries = _countryService.All().Countries;

                EditCityViewModel editCityVM = new EditCityViewModel();
                editCityVM.Id = id;
                editCityVM.CreateVM = createModel;
                return View(editCityVM);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditCityViewModel editViewModel)
        {
            City updCity = _cityService.FindBy(id);
            if (updCity == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                _cityService.Edit(id, editViewModel);
                return RedirectToAction(nameof(Index));
            }
            //invalid modelstate
            return View(editViewModel);
        }

        // GET: CityController/Delete/5
        public ActionResult Delete(int id)
        {
            City city = _cityService.FindBy(id);
            city.Citizens = _peopleService.FindByCity(id);
            return View(city);
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {

            bool result = _cityService.Remove(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            //lets go back to index anyhow
            return RedirectToAction(nameof(Index));
        }
    }
}
