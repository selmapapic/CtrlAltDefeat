using DigitalNomads.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalNomads.Models.Account
{
    public class AccountGetDetailsRes
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string TeamName { get; set; }
        public int TeamId { get; set; }


    }
}
