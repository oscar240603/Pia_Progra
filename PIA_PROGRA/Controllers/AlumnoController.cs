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

        [HttpDelete]
        [Route("BorrarInfo/{IdMatricula:int}")]
        public IActionResult BorrarInfo(int IdMatricula)
        {
            List<string> lstDatos = BL_Alumno.Borrar(Cadena, IdMatricula);

            if (lstDatos[0] == "00")
            {
                return Ok(new { codigo = "80", response = "Ok" });
            }
            else
            {
                return Ok(new { codigo = "14", response = lstDatos });
            }

        }

    }
}
