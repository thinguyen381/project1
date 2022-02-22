namespace OhNoAir.Models
{
    public class PaymentView
    {
        public int PaymentID { get; set; }
        public decimal? TotalAmount { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string Expiration { get; set; }
        public string CardHolder { get; set; }
        public int? DepartFlightID { get; set; }
        public int? ReturnFlightID { get; set; }

        public string Error { get; set; }

    }
}
