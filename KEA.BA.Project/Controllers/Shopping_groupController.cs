using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KEA.BA.Project.Models;

namespace KEA.BA.Project.Controllers
{
    public class Shopping_groupController : Controller
    {
        private Entities db = new Entities();

        // GET: Shopping_group
        public ActionResult Index()
        {
            var shopping_group = db.Shopping_group.Include(s => s.Store);
            return View(shopping_group.ToList());
        }

        // GET: Shopping_group/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shopping_group shopping_group = db.Shopping_group.Find(id);
            if (shopping_group == null)
            {
                return HttpNotFound();
            }
            return View(shopping_group);
        }


        // GET: Stores/Create
        public ActionResult Create()
        {
            ViewBag.city_ZIP = new SelectList(db.City, "zip", "city_name");
            return View();
        }

        //// POST: Shopping_group/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "store_ID,group_ID,group_size")] Shopping_group shopping_group)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Shopping_group.Add(shopping_group);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.store_ID = new SelectList(db.Store, "store_ID", "store_name", shopping_group.store_ID);
        //    return View(shopping_group);
        //}

        // POST: Shopping_group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "city_zip,store_size")] Store st)
        {
            _ = CreateandPopulate(st);

            return RedirectToAction("Index","Citizen_group");

        }

        //POST: Shopping_group/createpopulategroups
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateandPopulate([Bind(Include = "city_zip,store_size")] Store st)
        {

            int cityZip = (int)st.city_ZIP;
            int distance = (int)st.store_size;

            ArrayList storeList = new ArrayList(db.City.Find(cityZip).Store.ToList());
            ArrayList citizenList = new ArrayList(db.City.Find(cityZip).Citizen.ToList());
            CalculatorController cc = new CalculatorController();
            ArrayList groupList = new ArrayList();


            foreach (Store store in storeList)
            {
                int size = cc.CalculateGroupSizes((int)store.store_size, distance);
                //Create(size, (int)store.store_ID);
                Debug.WriteLine(store.store_ID);
                Shopping_group sg = new Shopping_group
                {
                    group_ID = store.store_ID,
                    group_size = size,
                    store_ID = store.store_ID
                };
                db.Shopping_group.Add(sg);
                groupList.Add(sg);
            }
            db.SaveChanges();
            //Citizen_groupController cgc = new Citizen_groupController();
            int index = 0;
            foreach (Citizen ci in citizenList)
            {
                Shopping_group sg = (Shopping_group)groupList[index];
                Citizen_group cg = new Citizen_group
                {
                    citizen_CPR = ci.CPR,
                    shopping_group_ID = sg.group_ID
                };
                db.Citizen_group.Add(cg);
                // cgc.Create(cg);

                if (index + 1 >= groupList.Count)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Shopping_group/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shopping_group shopping_group = db.Shopping_group.Find(id);
            if (shopping_group == null)
            {
                return HttpNotFound();
            }
            ViewBag.store_ID = new SelectList(db.Store, "store_ID", "store_name", shopping_group.store_ID);
            return View(shopping_group);
        }

        // POST: Shopping_group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "store_ID,group_ID,group_size")] Shopping_group shopping_group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopping_group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.store_ID = new SelectList(db.Store, "store_ID", "store_name", shopping_group.store_ID);
            return View(shopping_group);
        }

        // GET: Shopping_group/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shopping_group shopping_group = db.Shopping_group.Find(id);
            if (shopping_group == null)
            {
                return HttpNotFound();
            }
            return View(shopping_group);
        }

        // POST: Shopping_group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Shopping_group shopping_group = db.Shopping_group.Find(id);
            db.Shopping_group.Remove(shopping_group);
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
