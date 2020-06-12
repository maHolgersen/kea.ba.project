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
    public class Citizen_groupController : Controller
    {
        private Entities db = new Entities();

        // GET: Citizen_group
        public ActionResult Index()
        {
            var citizen_group = db.Citizen_group.Include(c => c.Citizen);
            return View(citizen_group.ToList());
        }

        // GET: Citizen_group/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citizen_group citizen_group = (Citizen_group)db.Citizen_group.Where(s => s.citizen_CPR == id).First();
            if (citizen_group == null)
            {
                return HttpNotFound();
            }
            return View(citizen_group);
        }

        // GET: Citizen_group/Create
        public ActionResult Create()
        {
            ViewBag.citizen_CPR = new SelectList(db.Citizen, "CPR", "citzen_name");
            return View();
        }

        // POST: Citizen_group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "citizen_CPR,shopping_group_ID")] Citizen_group citizen_group)
        {
            if (ModelState.IsValid)
            {
                db.Citizen_group.Add(citizen_group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.citizen_CPR = new SelectList(db.Citizen, "CPR", "citzen_name", citizen_group.citizen_CPR);
            return View(citizen_group);
        }

        // GET: Citizen_group/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citizen_group citizen_group = db.Citizen_group.Where(s => s.citizen_CPR == id).First();
            if (citizen_group == null)
            {
                return HttpNotFound();
            }
            ViewBag.citizen_CPR = new SelectList(db.Citizen, "CPR", "citzen_name", citizen_group.citizen_CPR);
            return View(citizen_group);
        }

        // POST: Citizen_group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "citizen_CPR,shopping_group_ID")] Citizen_group citizen_group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citizen_group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.citizen_CPR = new SelectList(db.Citizen, "CPR", "citzen_name", citizen_group.citizen_CPR);
            return View(citizen_group);
        }

        // GET: Citizen_group/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citizen_group citizen_group = (Citizen_group)db.Citizen_group.Where(s => s.citizen_CPR == id).First();
            if (citizen_group == null)
            {
                return HttpNotFound();
            }
            return View(citizen_group);
        }

        // POST: Citizen_group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Citizen_group citizen_group = (Citizen_group)db.Citizen_group.Where(s => s.citizen_CPR == id).First();
            db.Citizen_group.Remove(citizen_group);
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
