﻿namespace KhumaloCraft.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
