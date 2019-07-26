using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class EmailMessaging : IMessaging
    {
        public int Send<T>(T value) where T: IMessage
        {
            Message toSend = value as Message;
            Trace.TraceInformation(toSend.Content);
            Trace.TraceInformation(toSend.Header);
            Trace.TraceInformation(toSend.Recipient);
            return toSend.GetHashCode();
        }
    }
}