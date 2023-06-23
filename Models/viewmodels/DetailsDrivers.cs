using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UberApplication.Models.viewmodels
{
    public class DetailsDrivers
    {
     

        public RideDto SelectedDriver { get; set; }
        public IEnumerable<RideDto> KeptCars { get; set; }
   
}
}