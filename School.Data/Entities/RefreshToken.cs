using Microsoft.EntityFrameworkCore;

namespace School.Data.Entities
{

    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public bool IsActive => !IsExpired;
    }
}
