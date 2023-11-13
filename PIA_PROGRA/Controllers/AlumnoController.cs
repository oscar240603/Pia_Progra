using Microsoft.AspNetCore.Mvc;
using BLL;
using Entity;
using Microsoft.Extensions.Configuration; 
using System.Collections.Generic; 
using Microsoft.AspNetCore.Authorization;

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
            List<string> lstValidaciones = BL_Alumno.Validar(Cadena, alumno);

            if (lstValidaciones.Count == 0)
            {
                List<string> lstDatos = BL_Alumno.GuardarInfo(Cadena, alumno);

                if (lstDatos[0] == "00")
                {
                    return Ok(new { codigo = "80", response = "Ok" });
                }
                else
                {
                    return Ok(new { codigo = "14", response = lstDatos });
                }
            }
            else
            {
                return Ok(new { codigo = "14", response = lstValidaciones });
            }
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<DtoBuscar_Eliminar> lstalumno = BL_Alumno.MostrarAlumnos(Cadena);

            return Ok(new { codigo = "00", response = lstalumno });
        }


        [HttpPut]
        [Route("Modificar")]
        public IActionResult Modificar([FromBody] DtoAlumno alumno)
        {
            List<string> lstValidaciones = BL_Alumno.Validar(Cadena, alumno);

            if (lstValidaciones.Count == 0)
            {
                List<string> lstDatos = BL_Alumno.Editar(Cadena, alumno);

                if (lstDatos[0] == "00")
                {
                    return Ok(new { codigo = "80", response = "Ok" });
                }
                else
                {
                    return Ok(new { codigo = "14", response = lstDatos });
                }
            }
            else
            {
                return Ok(new { codigo = "14", response = lstValidaciones });
            }
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
