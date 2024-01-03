using Curso_1.Utils;
using Curso_1.Models;
using SAPbobsCOM;

namespace Curso_1.DataActions
{
    public class ConnectionContext
    {    
        public static Respuesta connectSAP(ConnectionParams _data)
        {

            SAPCompany.company ??= new SAPbobsCOM.Company();
            if (SAPCompany.company.Connected == true)
            {
                if (SAPCompany.company.CompanyDB != _data._database)
                {
                    SAPCompany.company.Disconnect();
                }
                else
                {
                    return new Respuesta
                    {
                        _code = Constants.OkCode,
                        _message = Constants.OkMessage
                    };
                }
            }

            SAPCompany.company.Server = _data._serverName;
            SAPCompany.company.CompanyDB = _data._database;
            SAPCompany.company.DbUserName = _data._dbuser;
            SAPCompany.company.DbPassword = _data._dbpassword;
            SAPCompany.company.UserName = _data._sapuser;
            SAPCompany.company.Password = _data._sappass;
            SAPCompany.company.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
            SAPCompany.company.LicenseServer = _data._license;

            int code = SAPCompany.company.Connect();

            if (code != Constants.Connected)
            {
                string message;
                SAPCompany.company.GetLastError(out code, out message);

                return new Respuesta
                {
                    _code = code.ToString(),
                    _message = message
                };
            }
            else
            {
                return new Respuesta
                {
                    _code = Constants.OkCode,
                    _message = Constants.OkMessage
                };
            }
        }
    }
}
