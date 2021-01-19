using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalNomads.Entities
{
    public class Duty
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsFinished { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public Account User { get; set; }
    }
}
