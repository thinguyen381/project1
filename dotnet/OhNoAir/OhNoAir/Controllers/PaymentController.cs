using Microsoft.AspNetCore.Mvc;
using OhNoAir.Data;
using OhNoAir.Models;
using OhNoAir.UseCase;

namespace OhNoAir.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext _context;
        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? DepartFlightID, int? ReturnFlightID)
        {
            Flight departFlight = DepartFlightID != null ? _context.Flight.First(f => f.FlightID == DepartFlightID) : null;
            Flight returnFlight = ReturnFlightID != null ? _context.Flight.First(f => f.FlightID == ReturnFlightID) : null;

            PaymentView payment = new PaymentView()
            {
                TotalAmount = (departFlight?.Price ?? 0) + (returnFlight?.Price ?? 0),
                DepartFlightID = DepartFlightID,
                ReturnFlightID = ReturnFlightID
            };



            return View(payment);
        }

        public IActionResult Pay(PaymentView payment)
        {
            Flight departFlight = payment?.DepartFlightID != null ? _context.Flight.First(f => f.FlightID == payment.DepartFlightID) : null;
            Flight returnFlight = payment?.ReturnFlightID != null ? _context.Flight.First(f => f.FlightID == payment.ReturnFlightID) : null;

            decimal TotalAmount = (departFlight?.Price ?? 0) + (returnFlight?.Price ?? 0);


            bool paySuccess = PayThrough3rd();

            if (!paySuccess)
            {
                payment.Error = "Error: Unable to pay using payment input!";
                return View("Index", payment);
            }

            var newPayment = new Payment
            {
                TotalAmount = TotalAmount,
                CardNumber = payment.CardNumber,
                CardHolder = payment.CardHolder,
                CVV = payment.CVV,
                Expiration = payment.Expiration
            };
            _context.Add(newPayment);
            _context.SaveChanges();

            var trackingID = Guid.NewGuid();
            var newOrder = new Reservation
            {
                TrackingID = trackingID,
                PaymentID = newPayment.PaymentID,
                DepartFlightID = departFlight.FlightID,
                ReturnFlightID = returnFlight?.FlightID
            };
            _context.Add(newOrder);
            _context.SaveChanges();

            List<Airport> destinations = _context.Airport.ToList();
            TrackingView tracking = new TrackingView
            {
                TrackingID = trackingID,
                DepartureFlight = departFlight,
                ReturnFlight = returnFlight,
                From = destinations.FirstOrDefault(d => d.AirportID == departFlight.FromID)?.AirportName,
                To = departFlight?.ToID != null ? destinations.FirstOrDefault(d => d.AirportID == departFlight.ToID)?.AirportName : null,

            };

            return View("Confirmation", tracking);
        }

        public IActionResult SendEmail(List<string>? email, Guid? TrackingID)
        {
            Reservation order = _context.Reservation.FirstOrDefault(o => o.TrackingID == TrackingID);
            if (order == null) return View("NotFound");

            List<Airport> destinations = _context.Airport.ToList();

            Flight departFlight = _context.Flight.FirstOrDefault(f => f.FlightID == order.DepartFlightID);
            Flight returnFlight = _context.Flight.FirstOrDefault(f => f.FlightID == order.ReturnFlightID);


            TrackingView tracking = new TrackingView
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

            return View("Confirmation", tracking);
        }

        private bool PayThrough3rd()
        {
            // Call 3rd party vendor to charge creditcard with TotalAmount
            return true;
        }
    }
}
