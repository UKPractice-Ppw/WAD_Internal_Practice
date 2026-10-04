using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using InternalPracticeApp.Models;

namespace InternalPracticeApp.Controllers
{
    public class StateController : Controller
    {
        dbHandler db = new dbHandler();
        // GET: State
        public ActionResult Index()
        {
            return View(db.GetAllState());
        }

        // GET: State/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: State/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: State/Create
        [HttpPost]
        public ActionResult Create(State st)
        {
            try
            {
                if(db.add_state(st))
                {
                    return RedirectToAction("Index","State");
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }  
            }
            catch
            {
                return View(st);
            }
        }

        // GET: State/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: State/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: State/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: State/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
