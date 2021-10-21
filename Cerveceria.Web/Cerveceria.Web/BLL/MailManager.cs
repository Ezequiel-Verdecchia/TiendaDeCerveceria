using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Cerveceria.Web.BLL
{
    public class MailManager
    {
        private string _emailOrigen { get; set; }
        private string _contraseña { get; set; }

        public MailManager()
        {
            var path = Path.GetFullPath(SystemEnums.Constantes.Path);
            var config = new ConfigurationManager(path);
            this._emailOrigen = config.GetValue<string>(SystemEnums.Constantes.Section, SystemEnums.Constantes.EmailOrigen);
            this._contraseña = config.GetValue<string>(SystemEnums.Constantes.Section, SystemEnums.Constantes.Password);
        }
        public void SendMail(string emailDestino, string asunto, string mensaje) 
        {
            var mailMessage = new MailMessage(_emailOrigen, emailDestino, asunto, mensaje)
            {
                IsBodyHtml = true
            };

            var oSmtpClient = new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                //oSmtpClient.Host = "smtp.gmail.com";
                Port = 587,
                Credentials = new System.Net.NetworkCredential(this._emailOrigen, this._contraseña)
            };

            oSmtpClient.Send(mailMessage);

            oSmtpClient.Dispose();
        }

    }
}
