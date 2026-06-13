using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class emailService
    {
        private MailMessage email;
        private SmtpClient server;

        public emailService()
        {
            server = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("utnprogramacionprueba@gmail.com", "wbid fklt bsaj jijb"),
                EnableSsl = true
            };
        }
        public void armarCorreo(string emailDestino, string asunto, string cuerpoHtml)
        {
            email = new MailMessage();
            email.From = new MailAddress("utnprogramacionprueba@gmail.com");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpoHtml;
        }


        public void enviarMail()
        {
            if (email == null)
                throw new InvalidOperationException("El correo no fue armado correctamente.");

            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
