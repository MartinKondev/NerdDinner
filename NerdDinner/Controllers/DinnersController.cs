using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        DinnerRepository repository = new DinnerRepository();

        // GET: /Dinners/
        public ActionResult Index()
        {
            var dinners = repository.FindUpcommingDinners().ToList();
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
        //
        //GET: /Dinners/Edit/2

        public ActionResult Edit(int id)
        {
            var dinner = repository.GetDinnerByID(id);
            return View(dinner);
        }

        //
        //POST: /Dinners/Edit/2
        [HttpPost]
        public ActionResult Edit(int id, string str)
        {
            var dinner = repository.GetDinnerByID(id);
            return View(dinner);
        }



        //Delete
        public ActionResult Delete(Dinner dinner)
        {
            repository.Delete(dinner);
            repository.Save();
            return View();
        }
    }
}
