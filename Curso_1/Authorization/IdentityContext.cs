using Curso_1.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Curso_1.Authorization
{
    public static class IdentityContext
    {
        public static ConnectionParams getCompanyParams(ClaimsIdentity identity)
        {
            ConnectionParams connectionParams = new ConnectionParams();
            connectionParams._serverName = identity.Claims.FirstOrDefault(x => x.Type == "ServerName").Value;
            connectionParams._database = identity.Claims.FirstOrDefault(x => x.Type == "DataBase").Value;
            connectionParams._dbuser = identity.Claims.FirstOrDefault(x => x.Type == "DbUser").Value;
            connectionParams._dbpassword = identity.Claims.FirstOrDefault(x => x.Type == "DBPass").Value;
            connectionParams._sapuser = identity.Claims.FirstOrDefault(x => x.Type == "SAPUser").Value;
            connectionParams._sappass = identity.Claims.FirstOrDefault(x => x.Type == "SAPPass").Value;
            connectionParams._license = identity.Claims.FirstOrDefault(x => x.Type == "LicenseServer").Value;

            string pattern = "\b";
            string replacement = @"\";

            connectionParams._serverName = Regex.Replace(connectionParams._serverName, pattern, replacement);

            return connectionParams;
        }
    }
}
