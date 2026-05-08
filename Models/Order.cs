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
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        //public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
