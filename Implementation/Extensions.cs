using Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public static class Extensions
    {
        public static string ToTimeLapseString(this Post value)
        {
            TimeSpan t = DateTime.Now - value.PostTime;
            string when = " (" +
                             (t.Days > 0 ? t.Days + " days" 
                           : (t.Hours > 0 ? " " + t.Hours + " hours " 
                           : (t.Minutes > 0 ? " " + t.Minutes + " minute "
                           : (t.Seconds > 0 ? " " + t.Seconds + " seconds" : ""))))
                           + " ago)";
            return value.PostText + when;
        }
 
    }
}
