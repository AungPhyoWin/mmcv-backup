using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyanmarCV.Models
{
    public class pdfCreation
    {
        public int ID { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }

        public string nationality { get; set; }
        public string passportNo { get; set; }
        public string nricNo { get; set; }
        public string mobileNo { get; set; }
        public string homePhoneNo { get; set; }
        public string contactableNo { get; set; }
        public string gender { get; set; }
        public string dateOfBirth { get; set; }
        public string countryOfResidence { get; set; }
        public string religion { get; set; }
        public string maritalStatus { get; set; }
        public string race { get; set; }
        public string yearOfWorkingExp { get; set; }
        public string highestEducationLevel { get; set; }
        public string latestPosition { get; set; }
        public string latestSalary { get; set; }
        public string expectedSalary { get; set; }
        public string availability { get; set; }
        public string insititution { get; set; }
        public string qualification { get; set; }
        public string fieldOfStudy { get; set; }
        public string gpa { get; set; }
        public string otherSkill { get; set; }
        public string language { get; set; }
        public string certificate { get; set; }

        public string companyName { get; set; }
        public string position { get; set; }
        public string monthlySalary { get; set; }
        public string jobResponsibility { get; set; }


    }

    public class pdfCreationContext : DbContext
    {
        public DbSet<pdfCreation> pdfCreations { get; set; }
    }
    public class pdfCVCreation
    {
       
    }
}