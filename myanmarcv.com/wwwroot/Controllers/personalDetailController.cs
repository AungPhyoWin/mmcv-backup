using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;

using System.Configuration;

using EO;
using EO.Pdf;
using EO.Pdf.Acm;
using EO.Pdf.Contents;
using EO.Pdf.Drawing;
using EO.Pdf.Internal;


using MyanmarCV.Models;

namespace MyanmarCV.Controllers
{ 
    public class personalDetailController : Controller
    {
        string webHostLink = ConfigurationManager.AppSettings["WebHostLink"].ToString();

        private mmCVDBContext db = new mmCVDBContext();
        private mmCVDataClassesDataContext db2 = new mmCVDataClassesDataContext();
        //
        // GET: /personalDetail/

        public ViewResult test()
        {
            return View();
        }

        public ViewResult Index()
        {
            return View(db.personalDetails.ToList());

        }

        public ViewResult GenerateReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateReport(personalDetail per)
        {
            string url1 = @"http://localhost:52524/pdfCreation/personalDetail?email=aung@pace-od.com";
            string url2 = @"http://localhost:52524/pdfCreation/WorkExpDetail?email=aung@pace-od.com";
           // ReportGenerating("test1", url1);
          //  ReportGenerating("test2", url2);

            PdfDocument doc1 = new PdfDocument(Server.MapPath("~/pdf/test1.pdf"));
            PdfDocument doc2 = new PdfDocument(Server.MapPath("~/pdf/test2.pdf"));
            
            PdfDocument doc = PdfDocument.Merge(doc1, doc2);
            doc.Save(Server.MapPath("~/pdf/test.pdf"));
            return View();
        }
        //
        public bool GenerateReport(string UserID)
        {
            string url1 = webHostLink + "/pdfCreation/personalDetail?email=" + UserID;
            string url2 = webHostLink + "/pdfCreation/WorkExpDetail?email=" + UserID;

            if (Directory.Exists(Server.MapPath("~/Resume/" + UserID)))
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Resume/" + UserID));

                foreach (string filePath in filePaths)
                {
                   System.IO.File.Delete(filePath);
                }

            }
            else
            {
                Directory.CreateDirectory(Server.MapPath("~/Resume/"+UserID));
            }

            ReportGenerating("test1",UserID,url1);
            ReportGenerating("test2",UserID,url2);

            PdfDocument doc1 = new PdfDocument(Server.MapPath("~/Resume/" +UserID + "/"+"test1"+".pdf"));
            PdfDocument doc2 = new PdfDocument(Server.MapPath("~/Resume/" + UserID + "/" +"test2"+ ".pdf"));

            PdfDocument doc = PdfDocument.Merge(doc1, doc2);
            doc.Save(Server.MapPath("~/Resume/"+UserID+"/"+UserID+".pdf"));
            return true;
        }

        public bool ReportGenerating(string outputFileName, string userID,string url)
        {
            try
            {
                EO.Pdf.Runtime.AddLicense(
                "E9zyBBDInbW4wdyzaay3x923dabw+g7kp+rp2g+9RoGkscufdePt9BDtrNzp" +
                "z+eupeDn9hnyntzCnrWfWZekzQzrpeb7z7iJWZekscufWZfA8g/jWev9ARC8" +
                "W7zTv/vjn5mkBxDxrODz/+ihbKW0s8uud4SOscufWbOz8hfrqO7CnrWfWZek" +
                "zRrxndz22hnlqJfo8h/kdpm1wtqyaaa2wdywW5f69h3youbyzs2yW5ezz7iJ" +
                "WZeksefyot7y8h/0q9zCwB3Bbtrr0xXjs+TI4vbKfMnq9drloLTBzdryot7y" +
                "8h/0q9zCnrW7aOPt9BDtrNzCnrV14+30EO2s3MKetZ9Zl6TNF+ic");
                //Get Page layout arguments
                string pageSizeName = "cbPageSize"; //args.GetString("cbPageSize");
                EO.Pdf.HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.A4;
                //SizeF pageSize = PdfPageSizes.FromName(pageSizeName);
                float marginLeft = 0.0f; //GetMargin(args, "txtMarginLeft", "Margin Left");
                float marginTop = 0.0f; //GetMargin(args, "txtMarginTop", "Margin Top");
                float marginRight = 0.0f; //GetMargin(args, "txtMarginRight", "Margin Right");
                float marginBottom = 0.0f; //GetMargin(args, "txtMarginBottom", "Margin Bottom");

                System.Drawing.RectangleF RectangleF = new System.Drawing.RectangleF(marginLeft, marginTop, 8.27f, 11.69f);

                HtmlToPdf.Options.OutputArea = RectangleF;

                HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.None;
                HtmlToPdf.Options.AutoFitY = HtmlToPdfAutoFitMode.None;
            }
            catch (Exception ex)
            {
                //CreateLogFiles.CreateLogFiles Err = new CreateLogFiles.CreateLogFiles();
                //Err.ErrorLog(Server.MapPath("~/Logs/ErrorLog"), ex);
            }

            //Convert the Url
            // HtmlToPdf.ConvertUrl(url, outputFileName);
            HtmlToPdf.Options.MaxLoadWaitTime = 120000;
            HtmlToPdf.ConvertUrl(url, Server.MapPath("~/resume/"+userID+"/"+outputFileName+".pdf"));
            //string testPath = Server.MapPath("~/pdf/"+outputFileName+".pdf");
            return true;

        }
        // GET: /personalDetail/Details/5

        public ViewResult Details(string email,int id=0)
        {
           
            
                personalDetail personaldetail = new personalDetail();
                //personalDetail personaldetail = db.personalDetails.Find(id);
                personaldetail = (from d in db2.personalDetails where d.email == email select d).SingleOrDefault();
                return View(personaldetail);
            
        }

        //
        // GET: /personalDetail/Create
        public Dictionary<int, string> GetNationalityDef()
        {
            var result = new Dictionary<int, string>();
             var test = (from d in db2.nationalityDefs
                               orderby d.nationalityId
                               select new KeyValuePair<int,string> (d.nationalityId.Value,d.nationality.ToString())).ToList();
            foreach (var item in test)
            {
                
                result.Add(item.Key,item.Value);
            }
            
            return result;
        }

        public Dictionary<int, string> GetMaritalDef()
        {
            var result = new Dictionary<int, string>();
            var Qry = (from d in db2.maritalDefs
                       orderby d.maritalStatusId
                       select new KeyValuePair<int,string> (d.maritalStatusId.Value,d.maritalStatusName.ToString())).ToList();
               foreach(var item in Qry)
               {
                    result.Add(item.Key,item.Value);
               }
               return result;
        }
        public ActionResult MyAccount()
        {

            //ViewBag.
            return View();
        }

        public PartialViewResult ResumeDownload()
        {
            //Thread.Sleep(2000);
           // var apw = "test";
            string email = Session["userID"].ToString();   // "aung@pace-od.com";
            GenerateReport(email);
            ViewBag.UserID = email;
            ViewBag.Extension = ".pdf";
            return PartialView("_ResumeDownload",ViewBag.UserID);

        }
        public ActionResult Create(string email,int id=0)
        {
           // ViewBag.email = email;
         //string email = Request.QueryString["email"];
            personalDetail per = new personalDetail();
            if (Session["userID"] != null)
            {
                email = Session["userID"].ToString();
                per = (from pers in db2.personalDetails where pers.email == email select pers).SingleOrDefault();

                var nationalList = new List<nationalityDef>();
                var nationalQry = (from d in db2.nationalityDefs
                                   orderby d.nationalityId
                                   select d.nationality).ToList();

                if (per != null)
                {
                    Dictionary<int, string> national = new Dictionary<int, string>();
                    national = GetNationalityDef();
                    ViewBag.nationalityId = new SelectList(db2.nationalityDefs, "nationalityId", "nationality", per.nationalityId.GetValueOrDefault());


                    //var maritalStatusList = new List<maritalDef>();
                    //var maritalStatusQry = (from d in db2.maritalDefs
                    //                        orderby d.maritalStatusId
                    //                        select d.maritalStatusName).ToList();

                    Dictionary<int, string> marital = new Dictionary<int, string>();
                    marital = GetMaritalDef();
                    ViewBag.maritalId = new SelectList(db2.maritalDefs, "maritalStatusId", "maritalStatusName", per.martialStatusId.GetValueOrDefault());
                    ViewBag.raceId = new SelectList(db2.raceDefs, "raceId", "raceName", per.race.GetValueOrDefault());

                    ViewBag.genderId = new SelectList(db2.genderDefs, "genderId", "genderName", per.genderId.GetValueOrDefault());
                    ViewBag.religionId = new SelectList(db2.religionDefs, "religionId", "religionName", per.religionId.GetValueOrDefault());
                    // ViewBag.maritalId = new SelectList(marital, marital.Keys.ToList().ToString(), marital.Values.ToList().ToString());
                    

                    return View(per);
                }
                return View(per);
            }
            else
            {

                ViewBag.nationalityId = new SelectList(db2.nationalityDefs, "nationalityId", "nationality");
                ViewBag.maritalId = new SelectList(db2.maritalDefs, "maritalStatusId", "maritalStatusName");
                ViewBag.raceId = new SelectList(db2.raceDefs, "raceId", "raceName");
                ViewBag.genderId = new SelectList(db2.genderDefs, "genderId", "genderName");
                ViewBag.religionId = new SelectList(db2.religionDefs, "religionId", "religionName");
                return View(per);
            }
            
        } 

        //
        // POST: /personalDetail/Create

        [HttpPost]
        public ActionResult Create(personalDetail personaldetail)
        {
            //if (ModelState.IsValid)
            //{
            //    db.personalDetails.Add(personaldetail);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");  
            //}
            if (ModelState.IsValid)
            {
                
                personalDetail per = (from pers in db2.personalDetails where pers.email == personaldetail.email select pers).SingleOrDefault();

                if (per != null)
                {

                    per.nationalityId = personaldetail.nationalityId;
                    per.passportNo = personaldetail.passportNo;
                    per.nricNo = personaldetail.nricNo;
                    per.homePhoneNo = personaldetail.homePhoneNo;
                    per.mobileNo = personaldetail.mobileNo;
                    per.contactableNo = personaldetail.contactableNo;
                    per.genderId = personaldetail.genderId;
                    per.dateOfBirth = personaldetail.dateOfBirth;
                    per.countryOfResidenceId = personaldetail.countryOfResidenceId;
                    per.religionId = personaldetail.religionId;
                    per.martialStatusId = personaldetail.martialStatusId;
                    per.race = personaldetail.race;
                    per.name = personaldetail.name;
                    per.modifiedDate = DateTime.Now;

                    Session["userID"] = per.email.ToString();
                }

                else
                {
                    Session["userID"] = personaldetail.email;
                    personaldetail.createdDate = DateTime.Now;

                    db2.personalDetails.InsertOnSubmit(personaldetail);
                }


                db2.SubmitChanges();

                return RedirectToAction("CareerProfile", "careerProfile", new { email = Session["userId"].ToString(), id = 0 });

                Dictionary<int, string> national = new Dictionary<int, string>();
                national = GetNationalityDef();
                ViewBag.nationalityId = new SelectList(db2.nationalityDefs, "nationalityId", "nationality");
                
            }
            return View();
        }
        
        //

        public ActionResult photoUpload()
        {
            return View();
        }


        [HttpPost]
        public ActionResult photoUpload(HttpPostedFileBase file)
        {
            string userID = Session["userID"].ToString(); //Request.QueryString["email"];
            if (file != null && file.ContentLength > 0)
            {
                string fileName = userID; //Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(file.FileName);
                fileName = fileName + fileExt;
                Directory.CreateDirectory(Server.MapPath("~/Uploads/" + userID));
                var path = Path.Combine(Server.MapPath("~/Uploads/"+userID), fileName);
                file.SaveAs(path);

                personalDetail dbPer = (from per in db2.personalDetails where per.email == userID select per).SingleOrDefault();

                dbPer.photo = fileName;
                db2.SubmitChanges();
            }

            return RedirectToAction(null);
        }

        // GET: /personalDetail/Edit/5
 
        public ActionResult Edit(string email,int id=0)
        {
            //personalDetail personaldetail = db.personalDetails.Find(id);
            personalDetail personaldetail = new personalDetail();
            //personalDetail personaldetail = db.personalDetails.Find(id);
            personaldetail = (from d in db2.personalDetails where d.email == email select d).SingleOrDefault();
               
            return View(personaldetail);
        }

        //
        // POST: /personalDetail/Edit/5

        [HttpPost]
        public ActionResult Edit(personalDetail personaldetail)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(personaldetail).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            personalDetail per = (from pers in db2.personalDetails where pers.email == personaldetail.email select pers).SingleOrDefault();

            if (per != null)
            {

                per.nationalityId = personaldetail.nationalityId;
                per.passportNo = personaldetail.passportNo;
                per.nricNo = personaldetail.nricNo;
                per.homePhoneNo = personaldetail.homePhoneNo;
                per.mobileNo = personaldetail.mobileNo;
                per.contactableNo = personaldetail.contactableNo;
                per.genderId = personaldetail.genderId;
                per.dateOfBirth = personaldetail.dateOfBirth;
                per.countryOfResidenceId = personaldetail.countryOfResidenceId;
                per.religionId = personaldetail.religionId;
                per.martialStatusId = personaldetail.martialStatusId;
                per.race = personaldetail.race;
                per.name = personaldetail.name;
                per.modifiedDate = DateTime.Now;

                Session["userID"] = per.email.ToString();
            }
            db2.SubmitChanges();
            return RedirectToAction("education", "CareerProfile", new { email = Session["userId"].ToString(), id = 0 });
            //return RedirectToAction("CareerProfile", "careerProfile", new { email = Session["userId"].ToString(), id = 0 });
            //return View(personaldetail);
        }

        //
        // GET: /personalDetail/Delete/5
 
        public ActionResult Delete(int id)
        {
            personalDetail personaldetail = db.personalDetails.Find(id);
            return View(personaldetail);
        }

        //
        // POST: /personalDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            personalDetail personaldetail = db.personalDetails.Find(id);
            db.personalDetails.Remove(personaldetail);
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