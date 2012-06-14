using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace MyanmarCV.Models
{
    public class AdminClass
    {
        private mmCVDataClassesDataContext db2 = new mmCVDataClassesDataContext();

        public int getLargestIdValue(int columnNo)
        {
           var dt = db2.nationalityDefs.OrderByDescending(c => c.nationalityId).FirstOrDefault();
           int largestId = 0;
           if (dt != null)
           {
               largestId = Convert.ToInt32(dt.nationalityId);
           }
          
            //int largestValue = (from dt in db2.countryDefs.Max());
            return largestId;
        }

    //    public string[] getStringArray(string csvString)
    //    { 
    //        string strline = "";
    //        string[] _values = null;
    //        int x = 0;
    //        //StreamReader sr = new StreamReader(csv);
    //          while (!sr.EndOfStream)
    //        {
    //            x++;
    //            strline = sr.ReadLine();
    //            _values = strline.Split(',');

    //            for (int i = 0; i < _values.Length; i++)
    //            {
    //                if (_values[i].Trim().Length > 0)
    //                {
    //                }
    //            }
    //        return 
    //    }
    }
}