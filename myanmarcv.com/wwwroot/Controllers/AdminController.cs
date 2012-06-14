using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using MyanmarCV.Models;

namespace MyanmarCV.Controllers
{ 
    public class AdminController : Controller
    {
        private mmCVDBContext db = new mmCVDBContext();
        private mmCVDataClassesDataContext db2 = new mmCVDataClassesDataContext();
        //
        // GET: /Admin/

        public ViewResult Index()
        {
            return View(db.mmCVs.ToList());
        }


        public ViewResult InsertCSV()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult InsertCSV(string fileName)
        {
            string path = Server.MapPath("~/pdf/" + "countries.csv");
            AdminClass adminClass = new AdminClass();
            int largestId = adminClass.getLargestIdValue(1);

            StreamReader sr = new StreamReader(path);
            string strline = "";
            string[] _values = null;
            int x = 0;
            while (!sr.EndOfStream)
            {
                x++;
                strline = sr.ReadLine();
                _values = strline.Split(',');

                for (int i = 0; i < _values.Length; i++)
                {
                    if (_values[i].Trim().Length > 0)
                    {
                        //subscribedEmail se = new subscribedEmail();
                        //countryDef countries = new countryDef();
                        nationalityDef countries = new nationalityDef();
                        countries.createdDate = DateTime.Now;
                        countries.nationality = _values[i];
                        countries.nationalityId = adminClass.getLargestIdValue(1) + 1;
                        //se.name = subEmail.name;
                        //se.mobileNo = subEmail.phoneNumber;

                        db2.nationalityDefs.InsertOnSubmit(countries);
                        db2.SubmitChanges();
                        //db2.countryDefs.InsertOnSubmit(countries);
                        //db2.SubmitChanges();
                    }
                }
                //if (_values[0].Trim().Length > 0)
                //{
                //    //MessageBox.show(_values[1]);
                //}
            }
            sr.Close();
            return View();
            
        }
        //
        // GET: /Admin/Details/5

        public ViewResult Details(int id)
        {
            mmCV mmcv = db.mmCVs.Find(id);
            return View(mmcv);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Create

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
        // GET: /Admin/Edit/5
 
        public ActionResult Edit(int id)
        {
            mmCV mmcv = db.mmCVs.Find(id);
            return View(mmcv);
        }

        //
        // POST: /Admin/Edit/5

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
        // GET: /Admin/Delete/5
 
        public ActionResult Delete(int id)
        {
            mmCV mmcv = db.mmCVs.Find(id);
            return View(mmcv);
        }

        //
        // POST: /Admin/Delete/5

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