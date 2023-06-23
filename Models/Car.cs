using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UberApplication.Models
{
    public class Car
    {

        [Key]
        public int CarID { get; set; }

        public string CarName { get; set; }

        public bool CarRemoved { get; set; }
    }

    public class CarDto
    {
        public int CarID { get; set; }

        public string CarName { get; set; }

        public bool CarRemoved { get; set; }
    }
}