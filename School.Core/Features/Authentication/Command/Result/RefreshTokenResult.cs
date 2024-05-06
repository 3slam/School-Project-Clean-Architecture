namespace School.Core.Features.Authentication.Command.Result
{
    public class RefreshTokenResult
    {
        public string Token { get; set; }
        public DateTime TokenExpirationDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiresOn { get; set; }

    }
}
