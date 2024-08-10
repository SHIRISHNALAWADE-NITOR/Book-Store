using Microsoft.AspNetCore.Mvc;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public EmailController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] MailData mailData)
        {
            if (mailData == null)
            {
                return BadRequest("Mail data is null.");
            }

            try
            {
                bool result = _notificationService.SendMail(mailData);
                if (result)
                {
                    return Ok("Email sent successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to send email.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
