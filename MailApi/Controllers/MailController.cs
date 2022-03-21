using MailApi.Models;
using MailApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {

        private readonly Mails m;

        public MailController(Mails _m) => m = _m;


        [HttpPost]
        [Route("Welcome")]
        [Consumes("application/json")]
        public IActionResult Welcome([FromBody] Generic generic)
        {
            if (generic != null)
            {
                if (m.SendWelcomeMail(generic))
                {
                    return Ok("Mail Sent");
                }

            }

            return BadRequest("Error in data");
        }



        [HttpPost]
        [Route("Sending")]
        [Consumes("application/json")]
        public IActionResult Sending([FromBody] Generic generic)
        {
            if (generic!= null)
            {
                var FormatName = "Generic.html";

                if (m.CreateFormat(generic.Format, FormatName))
                {
                    _ = m.SendMail(generic);

                    if (m.CleanFormat(FormatName))
                    {
                        return Ok("Mail Sent");
                    }
                    return BadRequest("Error Clean Format");
                }
                return BadRequest("Error Create Format");
            }

            return BadRequest("Error in data");
        }

    }
}
