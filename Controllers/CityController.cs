using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternalPracticeApp.Models;

namespace InternalPracticeApp.Controllers
{
    public class CityController : Controller
    {
        dbHandler db = new dbHandler();
        public List<SelectListItem> FillStateDDL()
        {
            string str = "SELECT * FROM tbl_State";
            DataTable dt = new DataTable();
            dt = db.ddlQuery(str);
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach(DataRow dr in dt.Rows)
            {
                statelist.Add(new SelectListItem {Text= Convert.ToString(dr.ItemArray[1]), Value= Convert.ToString(dr.ItemArray[0]) });
            }
            return statelist;
        }
        // GET: City
        public ActionResult Index(int? state_id)
        {
            ViewBag.StateList = FillStateDDL();
            List<City> cityList = db.GetCitiesByState(state_id);
            return View(cityList);
        }

        // GET: City/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: City/Create
        public ActionResult Create()
        {
            ViewBag.StateList = FillStateDDL();
            return View();
        }

        // POST: City/Create
        [HttpPost]
        public ActionResult Create(City ct)
        {
            try
            {
                if(db.add_city(ct))
                {
                    return RedirectToAction("Index","City");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }   
            }
            catch
            {
                ViewBag.StateList = FillStateDDL();
                return View(ct);
            }
        }

        // GET: City/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.StateList = FillStateDDL();
            return View();
        }

        // POST: City/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, City ct)
        {
            try
            {
                ViewBag.StateList = FillStateDDL();
                if(id!=0)
                {
                    db.Update_city(id,ct);
                    return RedirectToAction("Index","City");
                }
                else
                {
                    ViewBag.StateList = FillStateDDL();
                    return View(ct);
                }
                    // TODO: Add update logic here

                    
            }
            catch
            {
                return View();
            }
        }

        // GET: City/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: City/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, City ct)
        {
            try
            {
                if(db.DeleteCity(id))
                {
                    return RedirectToAction("Index","City");
                }
                else
                    return RedirectToAction("Index","State");
            }
            catch
            {
                return View();
            }
        }
    }
}
