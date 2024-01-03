using Curso_1.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Curso_1.Utils;

namespace Curso_1.Authorization
{
    public class TokenContext
    {
        public IConfiguration _configuration;
        public TokenContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static Respuesta CreateToken(IConfiguration _configuration, ConnectionParams _data)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {//En este paso comienzo a armar el token encapsulando a las variables que siguen. Las primeras 3 son obligatorias
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("ServerName",_data._serverName), //IP del servidor de base de datos
                        new Claim("DataBase",_data._database), //Nombre de la base de datos SAP
                        new Claim("DbUser",_data._dbuser), //Usuario de base de datos
                        new Claim("DBPass",_data._dbpassword), //Contraseña de base de datos
                        new Claim("SAPUser",_data._sapuser), //Usuario SAP
                        new Claim("SAPPass",_data._sappass), //Contraseña SAP
                        new Claim("LicenseServer",_data._license) //Servidor de licencias
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(60), //Esto es opcional. Indica que el token va a expirar
                signingCredentials: signIn
                );

            string _token = new JwtSecurityTokenHandler().WriteToken(token);

            return new Respuesta
            {
                _code = Utils.Constants.OkCode,
                _message = _token
            };

        }
    }
}
