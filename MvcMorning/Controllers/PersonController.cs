using MvcMorning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMorning.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }
        [NonAction]
        public ActionResult DetailPerson()
        {
            Person person = new Person();
            person.Id = 1;
            person.Name = "Ismaciil Ahmed";
            person.Addres = "Goljano";
            return View("DetailPerson",person);
        }

        public ActionResult ListPersons()
        {

            var listPerson = Person.GetListPerson();
            return View(listPerson);
        }

      
    }
}