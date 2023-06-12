using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UberApplication.Models
{
    public class Driver
    {
        [Key]
        public int DriverID { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }


        //A driver can ride many cars
        public ICollection<Ride> Rides { get; set; }

        
    }
}