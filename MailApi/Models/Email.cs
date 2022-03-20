using MailApi.Models.Interfaces;

namespace MailApi.Models
{
    public class Email : IEmail
    {
        public List<Addressee> Addressees { get; set; }
        public List<Addressee> AddresseesCC { get; set; }
        public List<Addressee> AddresseesBCC { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ScreenName { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
        public bool BodyIsHtml { get; set; }
        public List<Attached> Files { get; set; }
        public string MailServer { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
