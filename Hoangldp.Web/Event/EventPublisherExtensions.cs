using Hoangldp.Web.Data;
using Hoangldp.Web.Event.Data;

namespace Hoangldp.Web.Event
{
    public static class EventPublisherExtensions
    {
        public static void EntityInserted<T>(this IEventPublisher eventPublisher, T entity) where T : IEntity
        {
            eventPublisher.Publish(new EntityInserted<T>(entity));
        }

        public static void EntityUpdated<T>(this IEventPublisher eventPublisher, T entity) where T : IEntity
        {
            eventPublisher.Publish(new EntityUpdated<T>(entity));
        }

        public static void EntityDeleted<T>(this IEventPublisher eventPublisher, T entity) where T : IEntity
        {
            eventPublisher.Publish(new EntityDeleted<T>(entity));
        }
    }
}
