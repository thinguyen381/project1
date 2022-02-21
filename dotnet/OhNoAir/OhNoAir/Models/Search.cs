using System.ComponentModel.DataAnnotations;

namespace OhNoAir.Models
{
    public class Search
    {
        public List<Flight> Departs { get; set; }
        public List<Flight> Returns { get; set; }
        public List<Destination> Destinations { get; set; }
        public int From { get; set; }
        public string To { get; set; }
        public DateTime? DepartDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
