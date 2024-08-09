﻿using Microsoft.AspNetCore.Identity;

namespace BuySellCarsWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
