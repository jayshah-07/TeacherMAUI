using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherMAUI.Models
{
    public class GroupedScheduleItems : List<CombinedScheduleItem>
    {
        public string DayOfWeek { get; private set; }

        public GroupedScheduleItems(string dayOfWeek, List<CombinedScheduleItem> items) : base(items)
        {
            DayOfWeek = dayOfWeek;
        }
    }
}
