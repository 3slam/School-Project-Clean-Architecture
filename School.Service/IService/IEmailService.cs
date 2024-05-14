namespace School.Service.IService
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync
              (string ToEmail, string Subject, string Body, bool IsBodyHtml = false);
    }
}
