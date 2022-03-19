namespace OhNoAir.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public Guid? TrackingID { get; set; }
        public int PaymentID { get; set; }
        public int? DepartFlightID { get; set; }
        public int? ReturnFlightID { get; set; }
    }
}
