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
        public IActionResult Bienvenida([FromBody] Generic generic)
        {
            if (generic != null)
            {
                if (m.SendWelcomeMail(generic))
                {
                    return Ok("Correo Enviado");
                }

            }

            return BadRequest("Error en data");
        }
    }
}
