﻿namespace Domain.Entities
{
    public class OrderItem
    {
        public string OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
