using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalNomads.Entities
{
    public class MeditationWords
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int MeditationId { get; set; }
        public Meditation Meditation { get; set; }

    }
}
