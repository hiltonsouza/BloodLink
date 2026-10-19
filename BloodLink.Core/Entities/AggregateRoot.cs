using BloodLink.Core.Events;

namespace BloodLink.Core.Entities
{
    public class AggregateRoot : BaseEntity
    {
        private List<IDomainEvent> _events = new List<IDomainEvent>();
        public Guid Id  { get; protected set; }

        public IEnumerable<IDomainEvent> Events => _events;

        protected void AddEvent(IDomainEvent @event)
        {
            if(_events == null)
                _events = new List<IDomainEvent>();

            _events.Add(@event);
        }
    }
}
