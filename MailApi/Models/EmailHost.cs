using MailApi.Models.Interfaces;

namespace MailApi.Models
{
    public class EmailHost : IEmail
    {
        public string Host { get; set; }
        public List<Addressee> Addressees { get; set; }
        public List<Addressee> AddresseesCC { get; set; }
        public List<Addressee> AddresseesBCC { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string ScreenName { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
        public bool BodyIsHtml { get; set; }
        public List<Attached> Files { get; set; }
    }
}
