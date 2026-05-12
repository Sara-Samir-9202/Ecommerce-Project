namespace e_commerceAPI.DTO.Admin
{
    public class BannerDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string? Link { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}