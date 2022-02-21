﻿namespace OhNoAir.Models
{
    public class Flight
    {
        public int FlightID { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int FromID { get; set; }
        public int ToID { get; set; }
    }
}
