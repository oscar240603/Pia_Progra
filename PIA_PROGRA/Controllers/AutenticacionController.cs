using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL;
using Entity;

namespace PIA_PROGRA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string Secretkey;
        private readonly string Cadena;

        public AutenticacionController(IConfiguration config)
        {
            Secretkey = config.GetSection("settings").GetSection("secretkey").ToString();
            Cadena = config.GetConnectionString("PROD_Oscar");

        }

        [HttpPost]
        [Route("Token")]
        public IActionResult Token([FromBody] DtoToken UsuarioT)
        {
            bool Validacion = BL_Autenticacion.ValidaUsuarioInterfaces(Cadena, UsuarioT.Usuario, UsuarioT.Contra, UsuarioT.UGuid);

            if (Validacion)
            {

                var keyByte = Encoding.ASCII.GetBytes(Secretkey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, UsuarioT.Usuario));

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddSeconds(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256Signature)
                };

                var FechaExpira = tokenDescription.Expires;

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfing = tokenHandler.CreateToken(tokenDescription);

                string tokencreado = tokenHandler.WriteToken(tokenConfing);

                return Ok(new { access_token = tokencreado, Expires_Date = FechaExpira });
            }
            else
            {
                return Unauthorized(new { access_token = "" });
            }

        }
    }
}

