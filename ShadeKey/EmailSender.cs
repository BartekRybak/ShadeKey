using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace ShadeKey
{
    class EmailSender
    {
        private SmtpClient smtpClient;
        private MailAddress fromAdress;
        public EmailSender(string host,int port,string fromAdress,string password)
        {
            this.fromAdress = new MailAddress(fromAdress);
            smtpClient = new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(new MailAddress(fromAdress).Address, password)
            };
        }

        public bool SendMail(string toAdress,string subject,string body)
        {
            try
            {
                smtpClient.Send(new MailMessage(fromAdress.ToString(), toAdress, subject, body));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
