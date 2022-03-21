namespace OhNoAir.Models
{
    public class Book
    {
        public Flight DepartFlight { get; set; }
        public Flight ReturnFlight { get; set; }
        public decimal? TotalAmount { get; set; }
        public Passenger Passenger { get; set; }
    }
}
