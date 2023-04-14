using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Car_Invetory_Project.Models;

namespace Car_Invetory_Project.Controllers
{
    public class CarController : Controller
    {
        private SqlConnection db = new SqlConnection();

        // GET: Car
        public ActionResult Index()
        {
            if (Session["userid"] != null) {
                int userId = (int)Session["userid"]; // Get the userId from the session
                var cars = db.cars_master.Where(c => c.user_Id == userId).ToList(); // Get cars with matching userId
                return View(cars);
            }
            
            else
                return View("../user_master/create");
        }

        // GET: Car/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cars_master cars_master = db.cars_master.Find(id);
            if (cars_master == null)
            {
                return HttpNotFound();
            }
            return View(cars_master);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            if (Session["userid"] != null)
                return View();
            else
                return RedirectToAction("../User_master/create");
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,car_brand,car_model,year,price,isNew,user_Id,created_at,updated_at")] cars_master cars_master)
        {
            if (ModelState.IsValid)
            {
                cars_master.user_Id = Convert.ToInt32(Session["userid"]);
                db.cars_master.Add(cars_master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cars_master);
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cars_master cars_master = db.cars_master.Find(id);
            if (cars_master == null)
            {
                return HttpNotFound();
            }
            return View(cars_master);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,car_brand,car_model,year,price,isNew,user_Id,created_at,updated_at")] cars_master cars_master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cars_master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cars_master);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cars_master cars_master = db.cars_master.Find(id);
            if (cars_master == null)
            {
                return HttpNotFound();
            }
            return View(cars_master);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cars_master cars_master = db.cars_master.Find(id);
            db.cars_master.Remove(cars_master);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult OpenAddCar()
        {
            var carViewModel = new cars_master();
            return PartialView("create", carViewModel);
        } 
        public ActionResult OpenCarList()
        {
            var carViewModel = new cars_master();
            return PartialView("index", carViewModel);
        }
    }
}
