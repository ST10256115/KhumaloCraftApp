namespace KhumaloCraft.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
