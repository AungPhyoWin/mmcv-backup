using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyanmarCV.Models;

namespace MyanmarCV.Controllers
{ 
    public class mmCVController : Controller
    {
        private mmCVDBContext db = new mmCVDBContext();

        //
        // GET: /mmCV/

        public ViewResult Index()
        {
            return View(db.mmCVs.ToList());
        }

        //
        // GET: /mmCV/Details/5

        public ViewResult Details(int id)
        {
            mmCV mmcv = db.mmCVs.Find(id);
            return View(mmcv);
        }

        //
        // GET: /mmCV/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /mmCV/Create

        [HttpPost]
        public ActionResult Create(mmCV mmcv)
        {
            if (ModelState.IsValid)
            {
                db.mmCVs.Add(mmcv);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(mmcv);
        }
        
        //
        // GET: /mmCV/Edit/5
 
        public ActionResult Edit(int id)
        {
            mmCV mmcv = db.mmCVs.Find(id);
            return View(mmcv);
        }

        //
        // POST: /mmCV/Edit/5

        [HttpPost]
        public ActionResult Edit(mmCV mmcv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mmcv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mmcv);
        }

        //
        // GET: /mmCV/Delete/5
 
        public ActionResult Delete(int id)
        {
            mmCV mmcv = db.mmCVs.Find(id);
            return View(mmcv);
        }

        //
        // POST: /mmCV/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            mmCV mmcv = db.mmCVs.Find(id);
            db.mmCVs.Remove(mmcv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}