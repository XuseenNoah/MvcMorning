﻿using MvcMorning.Helpers;
using MvcMorning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMorning.Controllers
{
    [Authorize]
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


        //[ActionName("Persons")]
        ////[NonAction]

        //public ActionResult ListPersons()
        //{

        //    var listPerson = new List<Person>();
        //    return View("ListPersons", listPerson);
        //}

        [OutputCache(Duration =20)]
        public string GetDate()
        {
            return DateTime.Now.ToString();
        }

        //public ActionResult CreatePerson()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreatePerson(Person person)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Person.CreatePerson(person);
        //        TempData["SuccesfullySaved"] = "Succesfully Saved";
        //        ModelState.Clear();
        //    }
        //    return View();
        //}

        [HandleError]

        // [AllowAnonymous]
        [PermisionRequired(LoginEnter.Permissions.Add)]
        public ActionResult CreatePersons()
        {
            return View();
        }
        [OutputCache(Duration =10)]
        public string GetDAtes()
        {
            return DateTime.Now.ToString();
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
                    return RedirectToAction("ListPersons",new { name = person.Name });
                    
                }
                else
                {
                    TempData["GetPerson"] = "sdf";
                }
            }
            return View();
        }

        public ActionResult ListPersons(string personName,string phone)
        {
            var listPersons = Person.GetListPerson(personName,phone);
            return View(listPersons);
        }

        public ActionResult GetImage(string id)
        {
            var getImage = Person.GetImage(id);
            return File(getImage, "Image/jpg");
        }


        //public ActionResult DeletePerson(string id)
        //{
        //    var getPerson = Person.GetPersonDetail(id);
        //    return View(getPerson);
        //}

        //[HttpPost]
        //public ActionResult DeletePerson(Person person)
        //{
        //    Person.DeletePerson(person.Id);
        //    TempData["SuccesfullyDeleted"] = "s";
        //    return RedirectToAction("ListPersons");
        //}

        //public ActionResult DeletePerson(string id)
        //{
        //    Person.DeletePerson(id);
        //    TempData["SuccesfullyDeleted"] = "s";
        //    return RedirectToAction("Listpersons");

        //}

        public ActionResult DeletePerson(string id)
        {

            Person.DeletePerson(id);
            TempData["SuccesfullyDeleted"] = "s";
            return RedirectToAction("ListPersons");
        }

       


        public ActionResult UpdatePerson(string id)
        {
            var getPerson = Person.GetPersonDetail(id);
            return View(getPerson);
        }

        public ActionResult DetailPersons(string id)
        {
            var getPersonDetail = Person.GetPersonDetail(id);
            return View(getPersonDetail);
        }

        [HttpPost]
        public ActionResult UpdatePerson(Person person)
        {
            if (ModelState.IsValid)
            {
                Person.UpdatePerson(person);
                TempData["SuccesfullyUpdated"] = "s";
                ModelState.Clear();
                return RedirectToAction("ListPersons");
            }
            return View(person);
        }















    }
}