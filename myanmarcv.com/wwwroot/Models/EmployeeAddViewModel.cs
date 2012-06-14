using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyanmarCV.Models
{
    
    public class EmployeeAddViewModel
    {
        //public Employee employee { get; set; }
        //public Dictionary<int, string> staffTypes { get; set; }
        private mmCVDataClassesDataContext db2 = new mmCVDataClassesDataContext();

        public personalDetail perDetail { get; set; }
        public Dictionary<int, string> staffTypes { get; set; }

        public EmployeeAddViewModel() { }

        public EmployeeAddViewModel(int id)
        {
            foreach (var staffType in db2.nationalityDefs)
            {
                staffTypes.Add(staffType.nationalityId.Value, staffType.nationality);
            }
        }
    
    }
}