using Microsoft.AspNetCore.Mvc;
using OhNoAir.Data;
using OhNoAir.Models;

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
            var newOrder = new Order
            {
                TrackingID = trackingID,
                PaymentID = newPayment.PaymentID,
                DepartFlightID = departFlight.FlightID,
                ReturnFlightID = returnFlight?.FlightID
            };
            _context.Add(newOrder);
            _context.SaveChanges();

            return View("Confirmation", newOrder);
        }

        private bool PayThrough3rd()
        {
            // Call 3rd party vendor to charge creditcard with TotalAmount
            return true;
        }
    }
}
