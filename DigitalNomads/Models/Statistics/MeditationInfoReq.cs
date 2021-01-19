using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalNomads.Models.Statistics
{
    public class MeditationInfoReq
    {
        public int HoursOfSleep { get; set; }
        public int Day { get; set; }
        public int HoursFromPreviousBreak { get; set; }
        public int CurrentMood { get; set; }
        public int HoursFromPreviousMeditation { get; set; }
    }
}
