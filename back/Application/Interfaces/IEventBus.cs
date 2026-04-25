namespace Application.Interfaces
{
    public interface IEventBus<T>
    {
        public Task PublishAsync(T message);
    }
}
