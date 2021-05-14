using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_Data.Models.Service;
using MVC_Data.Models.ViewModel;
using MVC_Data.Models.Data;

namespace MVC_Data.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_languageService.All());
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            Language originalLanguage = _languageService.FindById(id);
            if (originalLanguage == null)
            {
                return RedirectToAction(nameof(Index));
            }

            EditLanguageViewModel viewModel = new EditLanguageViewModel(id, originalLanguage);
            viewModel.Speakers = _languageService.FindSpeakersByLanguageId(id);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LanguageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLanguageViewModel createLanguage)
        {
            if (ModelState.IsValid)
            {
                _languageService.Add(createLanguage);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(createLanguage);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Language originalLanguage = _languageService.FindById(id);
            if (originalLanguage == null)
            {
                return RedirectToAction(nameof(Index));
            }

            EditLanguageViewModel viewModel = new EditLanguageViewModel(id, originalLanguage);
            return View(viewModel);
        }

        // POST: LanguageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditLanguageViewModel viewModel)
        {
            Language updLanguage = _languageService.FindById(id);
            if (updLanguage == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                _languageService.Edit(id, viewModel);
                return RedirectToAction(nameof(Index));
            }
            //invalid modelstate
            return View(viewModel);
        }

        // GET: LanguageController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Language languageToDelete = _languageService.FindById(id);
            DeleteLanguageViewModel vm = new DeleteLanguageViewModel();
            vm.Language = languageToDelete;
            vm.Speakers = _languageService.FindSpeakersByLanguageId(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLanguage(int id)
        {
            bool result = _languageService.Remove(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            //lets go back to index anyhow
            return RedirectToAction(nameof(Index));
        }
    }
}
