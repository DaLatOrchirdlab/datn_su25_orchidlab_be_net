using orchid_backend_net.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace orchid_backend_net.Domain.Entities
{
    public class User : BaseIntEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName {  get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public int RoleID { get; set; }
        [ForeignKey(nameof(RoleID))]
        public virtual Role Role { get; set; }
        public DateTime Create_at {  get; set; }
        public string Create_by { get; set; }
        public string? AvatarUrl { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
