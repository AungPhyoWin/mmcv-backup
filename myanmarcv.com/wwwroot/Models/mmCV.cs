using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyanmarCV.Models
{
    public class mmCV
    {
        public int ID { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string desiredUserName { get; set; }
        
    }

    public class mmCVDBContext : DbContext
    {
        public DbSet<userInfo> UserInfo { get; set; }

        public DbSet<mmCV> mmCVs { get; set; }

        public DbSet<personalDetail> personalDetails { get; set; }

        public DbSet<careerProfile> careerProfiles { get; set; }

        public DbSet<employerRegistration> employerRegistrations { get; set; }
    }
}