﻿namespace KhumaloCraft.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
