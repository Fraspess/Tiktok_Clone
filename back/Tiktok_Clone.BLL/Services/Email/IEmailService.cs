namespace Tiktok_Clone.BLL.Services.Email
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body);
    }
}
