﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Customer : BaseEntity<string>
    {
        public string Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool IsRegistered { get; set; }
        public string? UserId { get; set; }
        [NotMapped]
        public IUser? User { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
