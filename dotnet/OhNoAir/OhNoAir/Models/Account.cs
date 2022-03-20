﻿using System.ComponentModel.DataAnnotations;

namespace OhNoAir.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
