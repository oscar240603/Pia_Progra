using Microsoft.AspNetCore.Mvc;
using BLL;
using Entity;

namespace PIA_PROGRA.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] DtoAlumno alumno)
        {
            return Ok();
        }

    }
}
