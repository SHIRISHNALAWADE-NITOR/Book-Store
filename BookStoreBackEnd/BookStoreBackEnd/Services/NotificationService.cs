using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class NotificationService : INotificationService
{
    private readonly IConfiguration _configuration;
    public NotificationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool SendMail(MailData Mail_Data)
    {
        try
        {
            //MimeMessage - a class from Mimekit
            MimeMessage email_Message = new MimeMessage();
            MailboxAddress email_From = new MailboxAddress(_configuration["EmailSettings:Name"], _configuration["EmailSettings:EmailId"]);
            email_Message.From.Add(email_From);
            MailboxAddress email_To = new MailboxAddress(Mail_Data.EmailToName, Mail_Data.EmailToId);
            email_Message.To.Add(email_To);
            email_Message.Subject = Mail_Data.EmailSubject;
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.TextBody = Mail_Data.EmailBody;
            email_Message.Body = emailBodyBuilder.ToMessageBody();
            //this is the SmtpClient class from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
            SmtpClient MailClient = new SmtpClient();
            string portString = _configuration["EmailSettings:Port"];
            string useSSLString = _configuration["EmailSettings:UseSSL"];
            MailClient.Connect(_configuration["EmailSettings:Host"], int.Parse(portString), SecureSocketOptions.StartTls);
            MailClient.Authenticate(_configuration["EmailSettings:EmailId"], _configuration["EmailSettings:Password"]);
            MailClient.Send(email_Message);
            MailClient.Disconnect(true);
            MailClient.Dispose();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error Occured While Sending mail");
        }
    }
}
