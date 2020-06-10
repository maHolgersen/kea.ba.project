using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KEA.BA.Project.Models;

namespace KEA.BA.Project.Controllers
{
    public class CitizensController : Controller
    {
        private Entities db = new Entities();

        // GET: Citizens
        public ActionResult Index()
        {
            var citizen = db.Citizen.Include(c => c.City);
            return View(citizen.ToList());
        }

        // GET: Citizens/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citizen citizen = db.Citizen.Find(id);
            if (citizen == null)
            {
                return HttpNotFound();
            }
            return View(citizen);
        }

        // GET: Citizens/Create
        public ActionResult Create()
        {
            ViewBag.city_zip = new SelectList(db.City, "zip", "city_name");
            return View();
        }

        // POST: Citizens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CPR,city_zip,citzen_name")] Citizen citizen)
        {
            if (ModelState.IsValid)
            {
                db.Citizen.Add(citizen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.city_zip = new SelectList(db.City, "zip", "city_name", citizen.city_zip);
            return View(citizen);
        }

        // GET: Citizens/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citizen citizen = db.Citizen.Find(id);
            if (citizen == null)
            {
                return HttpNotFound();
            }
            ViewBag.city_zip = new SelectList(db.City, "zip", "city_name", citizen.city_zip);
            return View(citizen);
        }

        // POST: Citizens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CPR,city_zip,citzen_name")] Citizen citizen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citizen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.city_zip = new SelectList(db.City, "zip", "city_name", citizen.city_zip);
            return View(citizen);
        }

        // GET: Citizens/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citizen citizen = db.Citizen.Find(id);
            if (citizen == null)
            {
                return HttpNotFound();
            }
            return View(citizen);
        }

        // POST: Citizens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Citizen citizen = db.Citizen.Find(id);
            db.Citizen.Remove(citizen);
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
    }
}
