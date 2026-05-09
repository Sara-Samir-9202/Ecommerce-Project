namespace e_commerceAPI.Models
{
    //public enum OrderStatus
    //{
    //    Pending,
    //    Paid,
    //    Shipped,
    //    Delivered,
    //    Cancelled
    //}
    public class Order
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        //public OrderStatus Status { get; set; } = OrderStatus.Pending;

        // أضف هذه الخصائص في كلاس Order
        public string? CustomerEmail { get; set; }    
        public string? CustomerName { get; set; }       
        public string? CustomerPhone { get; set; }     
        public string? ShippingAddress { get; set; }   
        public string Status { get; set; } = "Pending"; // Pending, Paid, Shipped, Delivered, Cancelled
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
