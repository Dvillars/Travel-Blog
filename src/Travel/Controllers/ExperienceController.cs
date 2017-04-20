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
    public class ExperienceController : Controller
    {
        private TravelContext db = new TravelContext();
        public IActionResult Index()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "Country", "Country");
            return View(db.Experiences.ToList());
        }

        public IActionResult PostDetails(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            ViewBag.Location = db.Locations.FirstOrDefault(locations => locations.LocationId == thisExperience.LocationId);
            ViewBag.People = db.People.Where(people => people.ExperienceId == thisExperience.ExperienceId).ToList();
            return View(thisExperience);
        }

        public IActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Experience experience)
        {
            db.Experiences.Add(experience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "City");
            return View(thisExperience);
        }

        [HttpPost]
        public IActionResult Edit(Experience experience)
        {
            db.Entry(experience).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
