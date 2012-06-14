using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyanmarCV.Models;

namespace MyanmarCV.Controllers
{
    public class PdfCreationController : Controller
    {
        //
        // GET: /Pdf/
        private mmCVDataClassesDataContext db2 = new mmCVDataClassesDataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonalDetail(string email,int i=0)
        {
            pdfCreation pdfCrea = new pdfCreation();
            pdfCrea = getPdfResume(email);
           
            return View(pdfCrea);
        }

        public ActionResult WorkExpDetail(string email, int i = 0)
        {
            
            pdfCreation pdfCrea = new pdfCreation();
            pdfCrea = getPdfResume(email);

            return View(pdfCrea);
        }

        public pdfCreation getPdfResume(string email)
        { 
             pdfCreation pdfCrea = new pdfCreation();
             personalDetail perDetail = (from perDetails in db2.personalDetails where perDetails.email == email select perDetails).SingleOrDefault();
             careerProfile carPro = (from carPros in db2.careerProfiles where carPros.email == email select carPros).SingleOrDefault();
             education edu = (from edus in db2.educations where edus.email == email select edus).SingleOrDefault();
             workExperience workExp = (from workExps in db2.workExperiences where workExps.email == email select workExps).SingleOrDefault();
             otherSkill otherSki = (from otherSkis in db2.otherSkills where otherSkis.email == email select otherSkis).SingleOrDefault();

             if (perDetail != null)
             {
                 pdfCrea.email = perDetail.email;
                 pdfCrea.nationality = perDetail.nationalityId.ToString();
                 pdfCrea.passportNo = perDetail.passportNo;
                 pdfCrea.nricNo = perDetail.nricNo;
                 pdfCrea.mobileNo = perDetail.mobileNo;
                 pdfCrea.homePhoneNo = perDetail.homePhoneNo;
                 pdfCrea.contactableNo = perDetail.contactableNo;
                 pdfCrea.gender = perDetail.genderId.ToString();
                 pdfCrea.dateOfBirth = perDetail.dateOfBirth.ToString();
                 pdfCrea.countryOfResidence = perDetail.countryOfResidenceId.ToString();
                 pdfCrea.religion = perDetail.religionId.ToString();
                 pdfCrea.maritalStatus = perDetail.martialStatusId.ToString();
                 pdfCrea.name = perDetail.name;
                 pdfCrea.city = perDetail.city;
                 pdfCrea.country = perDetail.countryId.ToString();
                 pdfCrea.address = perDetail.address;
             }

             if (carPro != null)
             {

                 pdfCrea.yearOfWorkingExp = carPro.yearOfWorkingExp;
                 pdfCrea.highestEducationLevel = carPro.highestEducationLevelId.ToString();
                 pdfCrea.latestPosition = carPro.latestPosition;
                 pdfCrea.latestSalary = carPro.latestSalary;
                 pdfCrea.expectedSalary = carPro.expectedSalary;
                 pdfCrea.availability = carPro.availablityId.ToString();
             }

             if (edu != null)
             {
                 pdfCrea.insititution = edu.institution;
                 pdfCrea.qualification = edu.qualification;
                 pdfCrea.fieldOfStudy = edu.fieldOfStudy;
             }

             if (workExp != null)
             {
                 pdfCrea.companyName = workExp.companyName;
                 pdfCrea.position = workExp.position;
                 pdfCrea.monthlySalary = workExp.monthlySalary;
                 pdfCrea.jobResponsibility = workExp.jobResponsibility;
             }

             if (otherSki != null)
             {
                 pdfCrea.otherSkill = otherSki.other;
                 pdfCrea.language = otherSki.language;
                 pdfCrea.certificate = otherSki.certificate;
             }
             return pdfCrea;
        }


        public string GetNationalityByID(int id)
        {
            
            return "";
        }

        public string GetCountryOfResidentByID(int id)
        {
            return "";
        }
        public string GetReligionByID(int id)
        {
            return "";
        }
        public string GetmartialStatusByID(int id)
        {
            return "";
        }
        public string GetCountryByID(int id)
        {
            return "";
        }

        public string GetHighestEducationLevelByID(int id)
        {
            return "";
        }
        public string GetAvailablityByID(int id)
        {
            return "";
        }
        
    }
}
