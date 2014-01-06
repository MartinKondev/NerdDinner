using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;
using PagedList;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        IDinnerRepository repository;

        public DinnersController(IDinnerRepository rep)
        {
            repository = rep;
        }

        public DinnersController() : this(new DinnerRepository())
        {
        }

        // GET: /Dinners/
        public ActionResult Index(int? page)
        {
            var dinners = repository.FindAllDinners().ToPagedList(page ?? 1, 10);
            return View(dinners);
        }

        //
        // GET: /Dinners/Details/2
        public ActionResult Details(int id)
        {
            var dinner = repository.GetDinnerByID(id);
            if (dinner==null)
            {
                return View("NotFound");
            }
            return View(dinner);
        }

        [Authorize]
        public ActionResult Create()
        {
            var dinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7),
                HostedBy = User.Identity.Name
            };
            return View(dinner);
        }

        //
        // POST: /Dinners/Create

        [HttpPost, Authorize]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                //dinner.HostedBy = User.Identity.Name;

                RSVP rsvp = new RSVP();
                rsvp.AttendeeName = User.Identity.Name;

                dinner.RSVPs = new EntitySet<RSVP>();
                dinner.RSVPs.Add(rsvp);

                repository.Add(dinner);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View(dinner);
        }



        //
        //GET: /Dinners/Edit/2

        public ActionResult Edit(int id)
        {
            var dinner = repository.GetDinnerByID(id);
            if (dinner.HostedBy != User.Identity.Name)
            {
                return View("InvalidOwner");
            }
            return View(dinner);
        }

        //
        //POST: /Dinners/Edit/2
        [HttpPost]
        public ActionResult Edit(int id, string str)
        {
            var dinner = repository.GetDinnerByID(id);
            if (dinner.HostedBy != User.Identity.Name)
            {
                return View("InvalidOwner");
            }

            return View(dinner);
        }



        //Delete
        public ActionResult Delete(Dinner dinner)
        {
            if (dinner.HostedBy != User.Identity.Name)
            {
                return View("InvalidOwner");
            }

            repository.Delete(dinner);
            repository.Save();
            return View();
        }
    }
}
