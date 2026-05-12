namespace e_commerceAPI.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public double Rate { get; set; }
        public int RatingCount { get; set; }
        public string CategoryName { get; set; }
    }
}
