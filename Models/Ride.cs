using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UberApplication.Models
{
    public class Ride
    {
        [Key]
        public int RideId { get; set; }
        public string RideName { get; set; }

        public int RideNum { get; set; }

        

        //one ride belongs to one car
        //Car can have many ride
        [ForeignKey("Cars")]
        public int CarID { get; set; }
        public virtual Car Cars { get; set; }

        //a car can be driven by many drivers
        public ICollection<Driver> Drivers { get; set; }
    }

    //todo
}