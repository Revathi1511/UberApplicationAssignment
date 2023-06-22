using UberApplication.Models;

namespace UberApplication.Controllers
{
    internal class DetailsRides
    {
        public RideDto SelectedRide { get; internal set; }
        public object ResponsibleDrivers { get; internal set; }
        public object AvailableDrivers { get; internal set; }
    }
}