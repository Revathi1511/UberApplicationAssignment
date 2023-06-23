using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UberApplication.Models.viewmodels
{
    public class updaterides
    { 
        //This viewmodel is a class which stores information that we need to present to /Rides/Update/{}

        //the existing Ride information

        public RideDto selectRide { get; set; }

        // all species to choose from when updating this ride

        public IEnumerable<RideDto> CarsOptions { get; set; }
    }
}
