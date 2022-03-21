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

        private bool SendMail(List<Addressee> to, List<Addressee> Copy, List<Addressee> HideCopy, string body, string screenName, string subject, string from, List<Attached> attacheds,bool isrelay)
        {
            if (isrelay)
            {
                var email = new EmailHost()
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
                Host = conf.GetSection("MailService").GetSection("Hostmail").Value,
                Files = attacheds

            };

                return sends.SendMailbyHost(email);
            }
            else
            {

                var email = new Email()
                {
                    IsSSL = Convert.ToBoolean(conf.GetSection("MailService").GetSection("EnableSSL").Value),
                    AddresseesCC = Copy,
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
           
        }

        public bool SendWelcomeMail(Generic generic)
        {
            var rp = System.IO.File.ReadAllText($"./Formats/Welcome.html");
            rp = rp.Replace("$FullName$", $"{generic.FirstName} {generic.LastName}");

            return SendMail(generic.Mail,generic.MailCopy,generic.MailHideCopy,rp,generic.Title,generic.Title,"MailApi",generic.Attacheds,generic.IsRelay);


        }

        public bool SendMail(Generic generic)
        {
            var rp = System.IO.File.ReadAllText($"./Formatos/Generico.html");
            rp = rp.Replace("$FirstName$", $"{generic.FirstName}")
                .Replace("$LastName$", $"{generic.LastName}")
                .Replace("$FullName$", $"{generic.FullName}")
                .Replace("$Title$", $"{generic.Title}")
                .Replace("$Generico0$", $"{generic.Generico0}")
                .Replace("$Generico1$", $"{generic.Generico1}")
                .Replace("$Generico2$", $"{generic.Generico2}")
                .Replace("$Generico3$", $"{generic.Generico3}")
                .Replace("$Generico4$", $"{generic.Generico4}")
                .Replace("$Generico5$", $"{generic.Generico5}")
                .Replace("$Generico6$", $"{generic.Generico6}")
                .Replace("$Generico7$", $"{generic.Generico7}")
                .Replace("$Generico8$", $"{generic.Generico8}")
                .Replace("$Generico9$", $"{generic.Generico9}")
                .Replace("$Generico10$", $"{generic.Generico10}")
                .Replace("$Generico11$", $"{generic.Generico11}")
                .Replace("$Generico12$", $"{generic.Generico12}")
                .Replace("$Generico13$", $"{generic.Generico13}")
                .Replace("$Generico14$", $"{generic.Generico14}")
                .Replace("$Generico15$", $"{generic.Generico15}")
                .Replace("$Generico16$", $"{generic.Generico16}")
                .Replace("$Generico17$", $"{generic.Generico17}")
                .Replace("$Generico18$", $"{generic.Generico18}")
                .Replace("$Generico19$", $"{generic.Generico19}")
                .Replace("$Generico20$", $"{generic.Generico20}");

            return SendMail(generic.Mail, generic.MailCopy, generic.MailHideCopy, rp, generic.Title, generic.Title, generic.Title, generic.Attacheds,generic.IsRelay);


        }

    }
}
