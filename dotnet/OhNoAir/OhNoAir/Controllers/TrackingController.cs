using Microsoft.AspNetCore.Mvc;
using OhNoAir.Data;
using OhNoAir.Models;
using OhNoAir.UseCase;
using System.Net;
using System.Net.Mail;

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

            List<Airport> destinations = _context.Airport.ToList();

            Flight departFlight = _context.Flight.FirstOrDefault(f => f.FlightID == order.DepartFlightID);
            Flight returnFlight = _context.Flight.FirstOrDefault(f => f.FlightID == order.ReturnFlightID);


            Tracking tracking = new Tracking
            {
                TrackingID = (Guid)trackingID,
                DepartureFlight = departFlight,
                ReturnFlight = returnFlight,
                From = destinations.FirstOrDefault(d => d.AirportID == departFlight.FromID)?.AirportName,
                To = departFlight?.ToID != null ? destinations.FirstOrDefault(d => d.AirportID == departFlight.ToID)?.AirportName : null,
            };




            return View(tracking);
        }

        public IActionResult SendEmail(List<string>? email, Guid? TrackingID)
        {
            Order order = _context.Order.FirstOrDefault(o => o.TrackingID == TrackingID);
            if (order == null) return View("NotFound");

            List<Airport> destinations = _context.Airport.ToList();

            Flight departFlight = _context.Flight.FirstOrDefault(f => f.FlightID == order.DepartFlightID);
            Flight returnFlight = _context.Flight.FirstOrDefault(f => f.FlightID == order.ReturnFlightID);


            Tracking tracking = new Tracking
            {
                TrackingID = (Guid)TrackingID,
                DepartureFlight = departFlight,
                ReturnFlight = returnFlight,
                From = destinations.FirstOrDefault(d => d.AirportID == departFlight.FromID)?.AirportName,
                To = departFlight?.ToID != null ? destinations.FirstOrDefault(d => d.AirportID == departFlight.ToID)?.AirportName : null,
                IsEmailSent = true
            };

            SendEmailUseCase emailUseCase = new SendEmailUseCase();
            emailUseCase.SendEmail(email, tracking);

            return View("Index",tracking);
        }
    }
}
