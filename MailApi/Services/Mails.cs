using MailApi.Models;

namespace MailApi.Services
{
    public class Mails
    {
        private readonly Sends sends;

        private readonly IConfiguration conf;

        private readonly IWebHostEnvironment host;


        public Mails(Sends _sends, IConfiguration _conf, IWebHostEnvironment _host)
        {
            sends = _sends;
            conf = _conf;
            host = _host;

        }

        public bool CreateFormat(string format, string name)
        {
            if (format != null || name != null)
            {
                var bytes = Convert.FromBase64String(format);
                File.WriteAllBytes($"./Formats/{name}", bytes);

                return true;
            }
            else
            {
                return false;
            }
        }


        public bool CleanFormat(string name)
        {
            if (name != null)
            {

                File.Delete($"./Formats/{name}");

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SendMail(List<Addressee> to, List<Addressee> Copy, List<Addressee> HideCopy, string body, string screenName, string subject, string from, List<Attached> attacheds)
        {

            var email = new Email()
            {
                IsSSL = Convert.ToBoolean(conf.GetSection("MailService").GetSection("EnableSSL").Value),
                AddresseesCC= Copy,
                AddresseesBCC = HideCopy,
                Body = body,
                BodyIsHtml = Convert.ToBoolean(conf.GetSection("MailService").GetSection("HTMLbody").Value),
                Addressees = to,
                ScreenName = conf.GetSection("MailService").GetSection("NombrePantalla").Value,
                Port = Convert.ToInt32(conf.GetSection("MailService").GetSection("PuertoCorreo").Value),
                Title = subject,
                Account = conf.GetSection("MailService").GetSection("correo").Value,
                Password = conf.GetSection("MailService").GetSection("Pass").Value,
                MailServer = conf.GetSection("MailService").GetSection("Hostmail").Value,
                Files = attacheds

            };
            return sends.SendMail(email);
        }

        public bool SendWelcomeMail(Generic generic)
        {
            var rp = System.IO.File.ReadAllText($"./Formats/Welcome.html");
            rp = rp.Replace("$FullName$", $"{generic.FirstName} {generic.LastName}");

            return SendMail(generic.Mail,generic.MailCopy,generic.MailHideCopy,rp,generic.Title,generic.Title,"MailApi",generic.Attacheds);


        }

    }
}
