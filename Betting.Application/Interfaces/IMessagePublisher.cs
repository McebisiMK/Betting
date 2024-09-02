namespace Betting.Application.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish<TMessage>(TMessage message);
    }
}
