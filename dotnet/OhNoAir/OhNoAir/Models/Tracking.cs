namespace OhNoAir.Models
{
    public class Tracking
    {
        public Flight DepartureFlight { get; set; }
        public Flight ReturnFlight { get; set; }
        public Guid TrackingID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
