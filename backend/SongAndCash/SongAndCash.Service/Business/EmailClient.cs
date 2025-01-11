using System.Net.Mail;
using Microsoft.Extensions.Options;
using SongAndCash.Model.Configuration;
using SongAndCash.Model.Entity;

namespace SongAndCash.Service.Business;

public class EmailClient : IEmailClient
{
    private readonly SmtpClient _smtpClient;

    public EmailClient(IOptions<GlobalConfiguration> configuration)
    {
        _smtpClient = new SmtpClient(configuration.Value.Email.Host, configuration.Value.Email.Port)
        {
            // MailHog doesn't require authentication
            UseDefaultCredentials = false,
            EnableSsl = false,
            // Set timeout (optional)
            Timeout = 10000,
        };
    }

    public async Task SendEmailToAdmin(RecoverableSale recoverableSale, string content)
    {
        using var message = new MailMessage();
        message.From = new MailAddress("from@example.com");
        message.Subject = "Song And Cash: Notificación";
        message.Body = content;
        message.IsBodyHtml = false;

        message.To.Add("user1@example.com");

        // // Add attachments if any
        // if (attachments != null)
        // {
        //     foreach (var attachment in attachments)
        //     {
        //         message.Attachments.Add(attachment);
        //     }
        // }

        try
        {
            await _smtpClient.SendMailAsync(message);
            Console.WriteLine($"Email sent successfully to user1@example.com");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            throw;
        }
    }
}
