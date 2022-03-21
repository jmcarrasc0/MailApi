using MailApi.Models;
using System.Net;
using System.Net.Mail;

namespace MailApi.Services
{
    public class Sends
    {

        public bool SendMail(Email mail)
        {

            try
            {


                SmtpClient server = new SmtpClient(mail.MailServer)
                {

                    Port = mail.Port,
                    EnableSsl = mail.IsSSL,
                    Credentials = new NetworkCredential(mail.Account, mail.Password)                    


                };

                var mssg = new MailMessage
                {
                    Subject = mail.Title,
                    From = new MailAddress(mail.Account, mail.ScreenName),
                    IsBodyHtml = mail.BodyIsHtml,
                    Body = mail.Body

                };



                foreach (var to in mail.Addressees)
                {
                    mssg.To.Add(new MailAddress(to.Mail, to.ShowName));
                }


                if (mail.AddresseesCC != null)
                {
                    foreach (var cc in mail.AddresseesCC)
                    {
                        mssg.CC.Add(new MailAddress(cc.Mail, cc.ShowName));
                    }
                }

                if (mail.AddresseesBCC != null)
                {

                    foreach (var bcc in mail.AddresseesBCC)
                    {
                        mssg.Bcc.Add(new MailAddress(bcc.Mail, bcc.ShowName));
                    }

                }

                if (mail.Files != null)
                {
                    foreach (var files in mail.Files)
                    {
                        var ms = new MemoryStream(files.File);
                        mssg.Attachments.Add(new Attachment(ms, files.Name, files.MediaType));
                    }
                }


                /* Send*/
                server.Send(mssg);
                return true;

            }
            catch (Exception ex)
            {

                var argEx = new ArgumentException("A problem occurred while sending mail", ex);
                throw argEx;
            }

        }

        public bool SendMailbyHost(EmailHost mail)
        {
            try
            {

                SmtpClient server = new SmtpClient()
                {
                    Host = mail.Host,
                    Port = mail.Port,
                    EnableSsl = mail.IsSSL,
                };


                var mssg = new MailMessage
                {
                    Subject = mail.Title,
                    From = new MailAddress(mail.From, mail.ScreenName),
                    Body = mail.Body,
                    IsBodyHtml = mail.BodyIsHtml

                };



                foreach (var to in mail.Addressees)
                {
                    mssg.To.Add(new MailAddress(to.Mail, to.ShowName));
                }


                if (mail.AddresseesCC != null)
                {
                    foreach (var cc in mail.AddresseesCC)
                    {
                        mssg.CC.Add(new MailAddress(cc.Mail, cc.ShowName));
                    }
                }

                if (mail.AddresseesBCC != null)
                {

                    foreach (var bcc in mail.AddresseesBCC)
                    {
                        mssg.Bcc.Add(new MailAddress(bcc.Mail, bcc.ShowName));
                    }

                }

                if (mail.Files != null)
                {
                    foreach (var files in mail.Files)
                    {
                        var ms = new MemoryStream(files.File);
                        mssg.Attachments.Add(new Attachment(ms, files.Name, files.MediaType));
                    }
                }


                /* Enviar */
                server.Send(mssg);
                return true;

            }
            catch (Exception ex)
            {

                var argEx = new ArgumentException("A problem occurred while sending mail", ex);
                throw argEx;
            }

        }
    }
}
