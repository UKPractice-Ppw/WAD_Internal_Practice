using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternalPracticeApp.Models;

namespace InternalPracticeApp.Controllers
{
    public class UserController : Controller
    {
        dbHandler db = new dbHandler();

        public List<SelectListItem> FillStateDDL()
        {
            List<SelectListItem> stateList = new List<SelectListItem>();
            string qry = "SELECT * FROM tbl_State";
            DataTable dt = new DataTable();
            dt= db.ddlQuery(qry);
            foreach(DataRow dr in dt.Rows)
            {
                stateList.Add(
                    new SelectListItem
                    {
                        Text= Convert.ToString(dr.ItemArray[1]),
                        Value = Convert.ToString(dr.ItemArray[0])
                    });
            }
            return stateList;
        }
        public JsonResult GetCitiesByState(int state_id)
        {
            List<object> citiList = new List<object>();
            DataTable dt = new DataTable();
            string qry = "SELECT * FROM tbl_City WHERE state_id="+state_id;
            dt = db.ddlQuery(qry);
            foreach(DataRow dr in dt.Rows)
            {
                citiList.Add(
                    new
                    {
                        Text= dr["city_name"].ToString(),
                        Value= dr["city_id"].ToString()
                    });
            }
            return Json(citiList,JsonRequestBehavior.AllowGet);
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
            
        }
        [HttpPost]
        public ActionResult Index(User ur)
        {
            if (db.Login_User(ur) != 0)
            {
                Session["User_id"] = db.Login_User(ur);
                Session["User_name"] = ur.user_name;
                return RedirectToAction("Create", "User");
            }
            else
            {
                return RedirectToAction("Create", "User");
            }
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            if (Session["User_name"] != null)
            {
                ViewBag.User_name = Session["User_name"].ToString();
                User model = new User
                {
                    States = FillStateDDL(),
                    Cities = new List<SelectListItem>()
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index","User");
            }
            
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User ur)
        {
            try
            {
                if(db.addUser(ur))
                {
                    return RedirectToAction("Create","City");
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }
            }
            catch
            {
                //ViewBag.StateList = FillStateDDL();
                return View(ur);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
