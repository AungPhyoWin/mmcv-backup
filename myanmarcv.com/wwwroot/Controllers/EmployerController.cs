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
    public class EmployerController : Controller
    {
        private mmCVDBContext db = new mmCVDBContext();
        private mmCVDataClassesDataContext db2 = new mmCVDataClassesDataContext();
        //
        // GET: /Employer/

        public ViewResult Index()
        {
           
            return View(db.employerRegistrations.ToList());
        }

        public ViewResult JobPost(string employerId)
        {
            //ViewBag.maritalId = new SelectList(db2.maritalDefs, "maritalStatusId", "maritalStatusName", per.martialStatusId.GetValueOrDefault());

            employerRegistration empRegistration = (from dt in db2.employerRegistrations where dt.employerid.ToString() == employerId.Trim() select dt).SingleOrDefault();
            jobAdvertisment jobAdver = new jobAdvertisment();
            jobAdver.employerid = empRegistration.employerid; // Guid.Parse(empRegistration.employerid.ToString());
            return View(jobAdver);
        }

        [HttpPost]
        public ActionResult JobPost(jobAdvertisment jobAdvert)
        {
            jobAdvert.createdDate = DateTime.Now;

            jobAdvert.jobAdvertismentId = getJobAdvertId();
            db2.jobAdvertisments.InsertOnSubmit(jobAdvert);
            db2.SubmitChanges();
            
            return RedirectToAction("ViewJobPost", "Employer", new {jobAdvertId = jobAdvert.jobAdvertismentId, id = 0 });
        }


        public string getJobAdvertId()
        {

            string jobAdvertId = Guid.NewGuid().ToString();
            jobAdvertId = jobAdvertId.Substring(24, 12).ToUpper();
            
            //int count = (from dt in db2.jobAdvertisments where dt.jobAdvertismentId == jobAdvertId select dt).Count();
            //if (count > 0)
            //{
            //    getJobAdvertId();
            //}
            //else {
            //    return jobAdvertId;
            //}
            return jobAdvertId.ToString();
        }


        public ViewResult ViewJobPost(string jobAdvertId)
        {
            jobAdvertisment jobAdvert = (from dt in db2.jobAdvertisments where dt.jobAdvertismentId == jobAdvertId.Trim() select dt).SingleOrDefault();
            if (jobAdvert != null)
            {
                return View(jobAdvert);
            }
            return View();
        }

        public ViewResult pdfViewJobPost(string jobAdvertId)
        {
            jobAdvertisment jobAdvert = (from dt in db2.jobAdvertisments where dt.jobAdvertismentId == jobAdvertId.Trim() select dt).SingleOrDefault();
            if (jobAdvert != null)
            {
                return View(jobAdvert);
            }
            return View();
        }


        public ViewResult EditJobPost(string jobAdvertId)
        {
            jobAdvertisment jobAdvert = (from dt in db2.jobAdvertisments where dt.jobAdvertismentId == jobAdvertId.Trim() select dt).SingleOrDefault();
            if (jobAdvert != null)
            {
                return View(jobAdvert);
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditJobPost(jobAdvertisment jobAdvert)
        {
            jobAdvertisment dbJobAdvert = (from dt in db2.jobAdvertisments where dt.jobAdvertismentId == jobAdvert.jobAdvertismentId.Trim() select dt).SingleOrDefault();

            dbJobAdvert = updateJobPost(dbJobAdvert, jobAdvert);

            // dbJobAdvert.yearOfExperienceRequired = "10";
            db2.SubmitChanges();
            

            return RedirectToAction("ViewJobPost", "Employer", new { jobAdvertId = jobAdvert.jobAdvertismentId, id = 0 });
        
        }

        public jobAdvertisment updateJobPost(jobAdvertisment oldJobAdvert,jobAdvertisment newJobAdvert)
        {
            oldJobAdvert.yearOfExperienceRequired = newJobAdvert.yearOfExperienceRequired;
            oldJobAdvert.jobPosition = newJobAdvert.jobPosition;
            oldJobAdvert.jobDescription = newJobAdvert.jobDescription;
            oldJobAdvert.closeDate = newJobAdvert.closeDate;
            oldJobAdvert.noOfemployeeRequired = newJobAdvert.noOfemployeeRequired;
            oldJobAdvert.jobTypeId = newJobAdvert.jobTypeId;
            oldJobAdvert.modifiedDate = DateTime.Now;
            //jobAdvertisment jobAdvert = new jobAdvertisment();
            //jobAdvert = newJobAdvert;
            return oldJobAdvert;
        }

        public ViewResult ViewJobPostList(string employerId)
        {

          List<jobAdvertisment> dbJobAdvertList = (from dt in db2.jobAdvertisments where dt.employerid.ToString() == employerId.Trim() select dt).ToList();
          return View(dbJobAdvertList);
        }
        //
        // GET: /Employer/Details/5

        public ViewResult Details(int id)
        {
            employerRegistration employerregistration = db.employerRegistrations.Find(id);
            return View(employerregistration);
        }

        //
        // GET: /Employer/Create

        public ActionResult Create()
        {
            ViewBag.maritalId = new SelectList(db2.maritalDefs, "maritalStatusId", "maritalStatusName");
            return View();
        } 

        //
        // POST: /Employer/Create

        [HttpPost]
        public ActionResult Create(employerRegistration employerregistration)
        {
            if (ModelState.IsValid)
            {
                db.employerRegistrations.Add(employerregistration);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(employerregistration);
        }
        
        //
        // GET: /Employer/Edit/5
 
        public ActionResult Edit(int id)
        {
            employerRegistration employerregistration = db.employerRegistrations.Find(id);
            return View(employerregistration);
        }

        //
        // POST: /Employer/Edit/5

        [HttpPost]
        public ActionResult Edit(employerRegistration employerregistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employerregistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employerregistration);
        }

        //
        // GET: /Employer/Delete/5
 
        public ActionResult Delete(int id)
        {
            employerRegistration employerregistration = db.employerRegistrations.Find(id);
            return View(employerregistration);
        }

        //
        // POST: /Employer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            employerRegistration employerregistration = db.employerRegistrations.Find(id);
            db.employerRegistrations.Remove(employerregistration);
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