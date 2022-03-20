namespace MailApi.Models.Interfaces
{
    public interface IEmail
    {
        List<Addressee> Addressees { get; set; }
        List<Addressee> AddresseesCC { get; set; }
        List<Addressee> AddresseesBCC { get; set; }
        string Title { get; set; }
        string Body { get; set; }
        string ScreenName { get; set; }
        int Port { get; set; }
        bool IsSSL { get; set; }
        bool BodyIsHtml { get; set; }
        List<Attached> Files { get; set; }
    }
}
