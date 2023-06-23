using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UberApplication.Models.viewmodels
{
    public class DetailsCars
    {
        public RideDto SelectedAnimal { get; set; }
        public IEnumerable<RideDto> ResponsibleDrivers { get; set; }

        public IEnumerable<RideDto> AvailableDrivers { get; set; }
    }
}