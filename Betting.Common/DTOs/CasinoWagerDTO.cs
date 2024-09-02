﻿namespace Betting.Common.DTOs
{
    public class CasinoWagerDTO
    {
        public string WagerId { get; set; }
        public string Game { get; set; }
        public string Provider { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
