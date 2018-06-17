namespace Hoangldp.Web.Event
{
    public interface IConsumer<T>
    {
        void HandleEvent(T eventMessage);
    }
}
