namespace WebApplication1.Models
{
    public class Message : IMessage
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public string Recipient { get; set; }
    }
}