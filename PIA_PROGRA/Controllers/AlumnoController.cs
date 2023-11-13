using Microsoft.AspNetCore.Mvc;

namespace PIA_PROGRA.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar()
        {

        }

    }
}
