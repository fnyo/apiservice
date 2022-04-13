namespace Fnyo.Learn.Jenkins.Toolkits.Email
{
    public class MailInfo
    {
        public string From { get; set; }    

        public string FromName { get; set; }
        public string[] Receivers { get; set; } 
        public string Subject { get; set; } 
        public string Content { get; set; } 
    }
}
