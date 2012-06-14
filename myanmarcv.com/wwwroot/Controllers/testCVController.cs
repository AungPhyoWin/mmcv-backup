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
    public class testCVController : Controller
    {
        private mmCVDBContext db = new mmCVDBContext();
        private mmCVDataClassesDataContext db2 = new mmCVDataClassesDataContext();

        //
        // GET: /testCV/

        public ViewResult Index()
        {
            return View(db.UserInfo.ToList());
        }

        //
        // GET: /testCV/Details/5

        public ViewResult Details(int id)
        {
            userInfo userinfo = db.UserInfo.Find(id);
            return View(userinfo);
        }

        //
        // GET: /testCV/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /testCV/Create

        [HttpPost]
        public ActionResult Create(userInfo userinfo)
        {
            //if (ModelState.IsValid)
            //{
            //    db.UserInfo.Add(userinfo);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");  
            //}

            //return View(userinfo);

            if (ModelState.IsValid)
            {
                db2.userInfos.InsertOnSubmit(userinfo);
                db2.SubmitChanges();
            }
            return View(userinfo);
        }
        
        //
        // GET: /testCV/Edit/5
 
        public ActionResult Edit(int id)
        {
            userInfo userinfo = db.UserInfo.Find(id);
            return View(userinfo);
        }

        //
        // POST: /testCV/Edit/5

        [HttpPost]
        public ActionResult Edit(userInfo userinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userinfo);
        }

        //
        // GET: /testCV/Delete/5
 
        public ActionResult Delete(int id)
        {
            userInfo userinfo = db.UserInfo.Find(id);
            return View(userinfo);
        }

        //
        // POST: /testCV/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            userInfo userinfo = db.UserInfo.Find(id);
            db.UserInfo.Remove(userinfo);
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