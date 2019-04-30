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
        //[NonAction]
        public ActionResult DetailPerson()
        {
            Person person = new Person();
            person.Id = 1;
            person.Name = "Ismaciil Ahmed";
            person.Addres = "Goljano";
            return View("DetailPerson", person);
        }


        [ActionName("Persons")]
        //[NonAction]

        public ActionResult ListPersons()
        {

            var listPerson = new List<Person>();
            return View("ListPersons", listPerson);
        }

        [OutputCache(Duration =20)]
        public string GetDate()
        {
            return DateTime.Now.ToString();
        }

        public ActionResult CreatePerson()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePerson(Person person)
        {
            if (ModelState.IsValid)
            {
                Person.CreatePerson(person);
                TempData["SuccesfullySaved"] = "Succesfully Saved";
                ModelState.Clear();
            }
            return View();
        }


        public ActionResult CreatePersons()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePersons(Person person)
        {
            if (ModelState.IsValid)
            {
                var getPerson = Person.GetPerson(person.Name);
                if (getPerson == null)
                {
                    Person.CreatePerson(person);
                    TempData["SuccesfullySaved"] = "S";
                    ModelState.Clear();
                }
                else
                {
                    TempData["GetPerson"] = "sdf";
                }
            }
            return View();
        }












    }
}