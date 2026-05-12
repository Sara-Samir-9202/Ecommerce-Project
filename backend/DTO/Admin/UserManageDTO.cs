namespace e_commerceAPI.DTO.Admin
{
    public class UserManageDTO
    {
        public string UserId { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
    }
}