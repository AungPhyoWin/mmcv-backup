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
    public class careerProfileController : Controller
    {
        private mmCVDBContext db = new mmCVDBContext();
        private mmCVDataClassesDataContext db2 = new mmCVDataClassesDataContext();
        //
        // GET: /careerProfile/
        public ActionResult CareerProfile(string email, int id = 0)
        {
            // ViewBag.email = email;
            //string email = Request.QueryString["email"];
            careerProfile careerPro = (from careers in db2.careerProfiles where careers.email == email select careers).SingleOrDefault();

            if (careerPro == null && email != "")
            {
                careerPro = new careerProfile();
                careerPro.email = email;
                careerPro.createDate = DateTime.Now;
                db2.careerProfiles.InsertOnSubmit(careerPro);
            }

            string photoName = (from perdetail in db2.personalDetails where perdetail.email == email select perdetail.photo).SingleOrDefault();
            string folder ="";
            string extension;
            if (photoName != "" && photoName != null)
            {
                extension = System.IO.Path.GetExtension(photoName);
                folder = photoName.Substring(0, photoName.Length - extension.Length);
            }

            ViewBag.folder = folder;
            ViewBag.dateTime = DateTime.Now.ToString();
            ViewBag.photoName = photoName;
            ViewBag.availablityId = new SelectList(db2.availablityDefs, "availablityId", "availablityName");

            ViewBag.highestEducationLevelId = new SelectList(db2.highestEducationLevelDefs,"highestEducationLevelId","highestEducationLevel");
            return View(careerPro);
        }

        [HttpPost]
        public ActionResult CareerProfile(careerProfile careerPro)
        {
            //if (ModelState.IsValid)
            //{
            //    db.personalDetails.Add(personaldetail);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");  
            //}
            careerProfile career = (from careers in db2.careerProfiles where careers.email == careerPro.email select careers).SingleOrDefault();

            if (career == null)
            {
                careerPro.createDate = DateTime.Now;
                db2.careerProfiles.InsertOnSubmit(careerPro);
                db2.SubmitChanges();
            }
            else
            {
                career.modifiedDate = DateTime.Now;
                career.highestEducationLevelId = careerPro.highestEducationLevelId;
                career.expectedSalary = careerPro.expectedSalary;
                career.availablityId = careerPro.availablityId;
                career.latestPosition = careerPro.latestPosition;
                career.latestSalary = careerPro.latestSalary;
                career.yearOfWorkingExp = careerPro.yearOfWorkingExp;
                db2.SubmitChanges();
            }

            return RedirectToAction("workExperience", "CareerProfile", new { email = Session["userId"].ToString(), id = 0 });
           // return View();
        }

        public ActionResult successfulResume()
        {
            return View();
        }

        public ActionResult Education(string email, int id = 0)
        {
            // ViewBag.email = email;
            //string email = Request.QueryString["email"];
            education educate = (from educates in db2.educations where educates.email == email select educates).SingleOrDefault();
            return View(educate);
        }

        [HttpPost]
        public ActionResult Education(education parEducation)
        {
            //if (ModelState.IsValid)
            //{
            //    db.personalDetails.Add(personaldetail);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");  
            //}
            education educate = (from educates in db2.educations where educates.email == parEducation.email select educates).SingleOrDefault();


            if (educate == null)
            {
                parEducation.createdDate = DateTime.Now;
                db2.educations.InsertOnSubmit(parEducation);
                db2.SubmitChanges();
            }
            else
            {
                educate.modifiedDate = DateTime.Now;
                educate.educationPeriodStart = parEducation.educationPeriodStart;
                educate.educationPeriodEnd = parEducation.educationPeriodEnd;
                educate.fieldOfStudy = parEducation.fieldOfStudy;
                educate.institution = parEducation.institution;
                educate.qualification = parEducation.qualification;
                db2.SubmitChanges();
            }
            return RedirectToAction("CareerProfile", "careerProfile", new { email = Session["userId"].ToString(), id = 0 });
            //return View();
        }

        public ActionResult workExperience(string email, int id = 0)
        {
            // ViewBag.email = email;
            //string email = Request.QueryString["email"];
            workExperience workexp = (from workexps in db2.workExperiences where workexps.email == email select workexps).SingleOrDefault();
            return View(workexp);
        }

        [HttpPost]
        public ActionResult workExperience(workExperience parWorkExp)
        {
            workExperience workexp = (from workexps in db2.workExperiences where workexps.email == parWorkExp.email select workexps).SingleOrDefault();


            if (workexp == null)
            {
                parWorkExp.createdDate = DateTime.Now;
                db2.workExperiences.InsertOnSubmit(parWorkExp);
                db2.SubmitChanges();
            }
            else
            {
                workexp.modifiedDate = DateTime.Now;
                workexp.employmentStartDate = parWorkExp.employmentStartDate;
                workexp.employmentEndDate = parWorkExp.employmentEndDate;
                workexp.companyName = parWorkExp.companyName;
                workexp.email = parWorkExp.email;
                workexp.jobResponsibility = parWorkExp.jobResponsibility;
                workexp.position = parWorkExp.position;
                workexp.monthlySalary = parWorkExp.monthlySalary;
                
                db2.SubmitChanges();
            }

            return RedirectToAction("otherSkill", "careerProfile", new { email = Session["userId"].ToString(), id = 0 });
        }

        public ActionResult otherSkill(string email, int id = 0)
        {
            // ViewBag.email = email;
            //string email = Request.QueryString["email"];
            otherSkill otherSki = (from otherSkis in db2.otherSkills where otherSkis.email == email select otherSkis).SingleOrDefault();
            return View(otherSki);
        }

        [HttpPost]
        public ActionResult otherSkill(otherSkill parOtherSkill)
        {
            otherSkill otherSki = (from otherSkis in db2.otherSkills where otherSkis.email == parOtherSkill.email select otherSkis).SingleOrDefault();


            if (otherSki == null)
            {
                parOtherSkill.createdDate = DateTime.Now;
                db2.otherSkills.InsertOnSubmit(parOtherSkill);
                db2.SubmitChanges();
            }
            else
            {
                otherSki.modifiedDate = DateTime.Now;
                otherSki.other = parOtherSkill.other;
                otherSki.language = parOtherSkill.language;
                otherSki.certificate = parOtherSkill.certificate;
                db2.SubmitChanges();
            }

            return RedirectToAction("successfulResume");
            
        }


        public ViewResult Index()
        {
            return View(db.careerProfiles.ToList());
        }

                //
        // GET: /careerProfile/Details/5

        public ViewResult Details(int id)
        {
            careerProfile careerprofile = db.careerProfiles.Find(id);
            return View(careerprofile);
        }

        //
        // GET: /careerProfile/Create

        public ActionResult Create()
        {
           
            return View();
        } 

        //
        // POST: /careerProfile/Create

        [HttpPost]
        public ActionResult Create(careerProfile careerprofile)
        {
            //if (ModelState.IsValid)
            //{
            //    db.careerProfiles.Add(careerprofile);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");  
            //}
           

            if (ModelState.IsValid)
            {
                db2.careerProfiles.InsertOnSubmit(careerprofile);
                db2.SubmitChanges();
            }
            
            return View(careerprofile);
        }
        
        //
        // GET: /careerProfile/Edit/5
 
        public ActionResult Edit(int id)
        {
            careerProfile careerprofile = db.careerProfiles.Find(id);
            return View(careerprofile);
        }

        //
        // POST: /careerProfile/Edit/5

        [HttpPost]
        public ActionResult Edit(careerProfile careerprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(careerprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(careerprofile);
        }

        //
        // GET: /careerProfile/Delete/5
 
        public ActionResult Delete(int id)
        {
            careerProfile careerprofile = db.careerProfiles.Find(id);
            return View(careerprofile);
        }

        //
        // POST: /careerProfile/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            careerProfile careerprofile = db.careerProfiles.Find(id);
            db.careerProfiles.Remove(careerprofile);
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