using CRUD_REST_API.Business.Services.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            _logger.LogInformation("E-poct gonderilmesi basladi. Unvan: {email}", toEmail);
            await Task.Delay(3000);

            _logger.LogInformation("E-poct ugurla gonderildi: {email}", toEmail);
        }
    }
}
