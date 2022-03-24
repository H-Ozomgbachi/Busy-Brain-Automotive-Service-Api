using System;

namespace Common.Data.Domain
{
    public abstract class Entity
    {
        private readonly Action<IEvent> _applier;
        protected Entity(Action<IEvent> applier) => _applier = applier;

        protected void RaiseEvent(IEvent @event)
        {
            _applier(@event);
        }
    }
}
