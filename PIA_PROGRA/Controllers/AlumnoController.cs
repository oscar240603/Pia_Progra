using Microsoft.AspNetCore.Mvc;
using BLL;
using Entity;

namespace PIA_PROGRA.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {

        private readonly string Cadena;

        public AlumnoController(IConfiguration Config)
        {
            Cadena = Config.GetConnectionString("PROD_Oscar");
        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] DtoAlumno alumno)
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<DtoAlumno> lstalumno = BL_Alumno.MostrarAlumnos(Cadena);

            return Ok(new { codigo = "00", response = lstalumno });
        }

    }
}
