﻿namespace Store.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Quantity { get; set; }

        public string Unit { get; set; } = null!;
    }
}