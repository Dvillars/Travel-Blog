using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Travel.Controllers
{
    public class PersonController : Controller
    {
        private TravelContext db = new TravelContext();
        public IActionResult Index()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View(db.People.ToList());
        }
        [HttpPost]
        public IActionResult Index(Location location)
        {
            var QueriedExperiences = db.Experiences.Where(experiences => experiences.LocationId == location.LocationId).ToList();
            List<Person> People = new List<Person> { };
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            foreach (var QueriedExperience in QueriedExperiences)
            {
                var QueriedPeople = db.People.Where(people => people.ExperienceId == QueriedExperience.ExperienceId);
                foreach (var QueriedPerson in QueriedPeople)
                {
                    People.Add(QueriedPerson);
                }
            }
            return View(People);
        }

        public IActionResult Create()
        {
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Description");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person person)
        {
            db.People.Add(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var thisPerson = db.People.FirstOrDefault(people => people.PersonId == id);
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Description");
            return View(thisPerson);
        }

        [HttpPost]
        public IActionResult Edit(Person person)
        {
            db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}