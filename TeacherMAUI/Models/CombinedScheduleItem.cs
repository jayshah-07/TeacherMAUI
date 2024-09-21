using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherMAUI.Models
{
    public class CombinedScheduleItem
    {
        public string Day { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public string Type { get; set; } // "Efhmeria" or "Exei"
        public string Details { get; set; } // Location for Efhmeria, Lesson and Tmima for Exei
        public object OriginalItem { get; set; } // Reference to the original Efhmeria or Exei item
    }
}
