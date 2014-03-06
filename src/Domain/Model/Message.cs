using System.Net.Mail;

namespace Reconfig.Domain.Model
{
    public class Message
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public MailPriority Priority { get; set; }
        public string AppliedTemplate { get; set; }
    }
}
