using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IMessaging
    {
        int Send<T>(T value) where T : IMessage;
    }
}