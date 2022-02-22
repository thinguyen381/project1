using Microsoft.AspNetCore.Mvc;
using OhNoAir.Data;
using OhNoAir.Models;

namespace OhNoAir.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext _context;
        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? DepartFlightID, int? ReturnFlightID)
        {
            Flight departFlight = DepartFlightID != null ? _context.Flight.First(f => f.FlightID == DepartFlightID) : null;
            Flight returnFlight = ReturnFlightID != null ? _context.Flight.First(f => f.FlightID == ReturnFlightID) : null;

            Book book = new Book()
            {
                DepartFlight = departFlight,
                ReturnFlight = returnFlight,
                TotalAmount = (departFlight?.Price ?? 0) + (returnFlight?.Price ?? 0)
            };



            return View(book);
        }
    }
}
