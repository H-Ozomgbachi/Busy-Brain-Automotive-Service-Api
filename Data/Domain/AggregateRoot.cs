using System.Collections.Generic;
using System.Linq;

namespace Common.Data.Domain
{
    public abstract class AggregateRoot
    {
        protected List<IEvent> _uncommitedEvents = new List<IEvent>();
        protected virtual void RaiseEvent(IEvent @event)
        {
            _uncommitedEvents.Add(@event);
        }
        public IReadOnlyCollection<IEvent> GetUncommitedEvents()
        {
            return _uncommitedEvents.ToList().AsReadOnly();
        }
        public void ClearUncommitedEvents()
        {
            _uncommitedEvents.Clear();
        }
    }
}
