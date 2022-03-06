using Microsoft.AspNetCore.Mvc;
using OhNoAir.Data;
using OhNoAir.Models;
using System.Data.SqlClient;
using System.Linq;

namespace OhNoAir.Controllers
{
    public class SearchController : Controller
    {
      
        private readonly ApplicationDbContext _context;
        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? from, int? to, DateTime? departDate, DateTime? returnDate)
        {
            var search = new Search()
            {
                Airports = _context.Airport.ToList(),
                DepartDate = departDate ?? DateTime.Now,
                ReturnDate = returnDate ?? DateTime.Now
            };
        

            if (from != null && to != null)
            { 
                search.Departs = _context.Flight.Where(f => f.FromID == from && f.ToID == to && f.Date == departDate).ToList();
                search.Returns = _context.Flight.Where(f => f.FromID == to && f.ToID == from && f.Date == returnDate).ToList();
            }

            return View(search);

        }




    }
}
