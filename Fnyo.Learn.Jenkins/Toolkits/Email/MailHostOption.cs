namespace Fnyo.Learn.Jenkins.Toolkits.Email
{
    public class MailHostOption
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; } = true;
        public string UserName { get; set; }    

        public string AuthCode { get; set; }    

    }
}
