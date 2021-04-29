using Microsoft.AspNetCore.Mvc;
using MVC_Data.Models.Data;
using MVC_Data.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Controllers
{
    public class SPAController : Controller
    {
        private IPeopleService _service = new PeopleService();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DisplayPeople()
        {
            ViewData["ShowPersonDeleteButton"] = true;
            ViewData["ShowPersonEditButton"] = false;
            ViewData["ShowPersonDetailsButton"] = true;
            return PartialView("_peoplePartialView", _service.All().Persons);
        }

        [HttpPost]  //details for input id
        public IActionResult Details(int id)
        {
            Person foundPerson = _service.FindBy(id);

            ViewData["ShowPersonDeleteButton"] = true;
            ViewData["ShowPersonEditButton"] = false;
            ViewData["ShowPersonDetailsButton"] = true;
            ViewData["ShowHeader"] = true;
            return PartialView("_personPartialView", foundPerson);
        }

        [HttpPost]
        public IActionResult RemovePerson(int id)
        {
            Person personToRemove = _service.FindBy(id);
            if (personToRemove != null)
            {
                if (_service.Remove(id))
                {
                    return Ok("Person with id " + id + " has been removed.");
                }
            }
            return NotFound("Person with id " + id + " was not found.");
        }

        [HttpPost]
        public IActionResult PartialDetails(int id)
        {
            Person foundPerson = _service.FindBy(id);


            return PartialView("_personDetailsView", foundPerson);
        }

        [HttpPost]
        public IActionResult PartialPersonRow(int id)
        {
            Person foundPerson = _service.FindBy(id);

            ViewData["ShowPersonDeleteButton"] = true;
            ViewData["ShowPersonEditButton"] = false;
            ViewData["ShowPersonDetailsButton"] = true;
            ViewData["ShowHeader"] = false;
            return PartialView("_personPartialView", foundPerson);
        }
    }
}
