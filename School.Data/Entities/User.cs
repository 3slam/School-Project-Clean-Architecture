using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace School.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }

        public RefreshToken? RefreshToken { get; set; }

        [EncryptColumn]
        public string Code { get; set; }
    }
}
