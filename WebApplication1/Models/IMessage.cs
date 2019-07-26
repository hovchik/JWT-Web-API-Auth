namespace WebApplication1.Models
{
    public interface IMessage
    {
        string Header { get; set; }
        string Content { get; set; }
        string Recipient { get; set; }
    }
}