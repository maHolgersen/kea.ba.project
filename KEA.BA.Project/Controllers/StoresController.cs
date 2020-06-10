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
    public class StoresController : Controller
    {
        private Entities db = new Entities();

        // GET: Stores
        public ActionResult Index()
        {
            var store = db.Store.Include(s => s.City);
            return View(store.ToList());
        }

        // GET: Stores/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            ViewBag.city_ZIP = new SelectList(db.City, "zip", "city_name");
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "store_ID,store_name,city_ZIP,store_address,store_size")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Store.Add(store);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.city_ZIP = new SelectList(db.City, "zip", "city_name", store.city_ZIP);
            return View(store);
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            ViewBag.city_ZIP = new SelectList(db.City, "zip", "city_name", store.city_ZIP);
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "store_ID,store_name,city_ZIP,store_address,store_size")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.city_ZIP = new SelectList(db.City, "zip", "city_name", store.city_ZIP);
            return View(store);
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Store store = db.Store.Find(id);
            db.Store.Remove(store);
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
