namespace OhNoAir.Models
{
    public class Search
    {
        public List<Flight> Departs { get; set; }
        public List<Flight> Returns { get; set; }

        public string Depart { get; set; }
        public string Return { get; set; }

    }
}
