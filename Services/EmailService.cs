using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using InsuranceProviderManagementSystem.Models;

namespace InsuranceProviderManagementSystem.Services
{
    public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailWithAttachment(string filePath, string fileName, Documents model)
    {
        // Load SMTP configuration from appsettings.json
        var smtpHost = _config["EmailSettings:SMTPHost"];
        var smtpPort = int.Parse(_config["EmailSettings:SMTPPort"]);
        var senderEmail = _config["EmailSettings:SenderEmail"];
        var senderPassword = "fhydhuyimxodtciw"; //Ram-uvkb fgta pkzn jice Mail-fhydhuyimxodtciw
        var recipientEmail = ((model!=null) ? model.ToEmailAddress : "vij1557095@gmail.com"); // Set recipient email

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Sender Name", senderEmail));
        message.To.Add(new MailboxAddress("Recipient Name", recipientEmail));
        message.Subject = model.Subject;

        // Set email body and attachment
        var body = new TextPart("plain") { Text = model.Message };
        var attachment = new MimePart("application", "pdf")
        {
            Content = new MimeContent(File.OpenRead(filePath), ContentEncoding.Default),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = fileName
        };

        var multipart = new Multipart("mixed") { body, attachment };
        message.Body = multipart;

        // Connect to Gmail SMTP server and send the email
        using (var client = new SmtpClient())
        {
            client.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(senderEmail, senderPassword);

            await client.SendAsync(message);
            client.Disconnect(true);
        }
    }
}
}
