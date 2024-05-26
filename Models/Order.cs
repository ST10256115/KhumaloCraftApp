namespace KhumaloCraft.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; 


        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }



    }
}
