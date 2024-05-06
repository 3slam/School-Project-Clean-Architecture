namespace School.Data.Entities
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Message { get; set; }

        public RefreshToken RefreshToken = new RefreshToken();
    }
}
