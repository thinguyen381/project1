using Microsoft.AspNetCore.Mvc;
using OhNoAir.Data;
using OhNoAir.Models;

namespace OhNoAir.Controllers
{
    public class TrackingController : Controller
    {
        private ApplicationDbContext _context;
        public TrackingController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(Guid? trackingID)
        {
            if (trackingID == null) return View();
            
            Order order = _context.Order.FirstOrDefault(o => o.TrackingID == trackingID);
            if (order == null) return View("NotFound");

            List<Destination> destinations = _context.Destination.ToList();

            Flight departFlight = _context.Flight.FirstOrDefault(f => f.FlightID == order.DepartFlightID);
            Flight returnFlight = _context.Flight.FirstOrDefault(f => f.FlightID == order.ReturnFlightID);


            Tracking tracking = new Tracking
            {
                TrackingID = (Guid)trackingID,
                DepartureFlight = departFlight,
                ReturnFlight = returnFlight,
                From = destinations.FirstOrDefault(d => d.DestinationID == departFlight.FromID)?.DestinationName,
                To = departFlight?.ToID != null ? destinations.FirstOrDefault(d => d.DestinationID == departFlight.ToID)?.DestinationName : null,
            };




            return View(tracking);
        }
    }
}
