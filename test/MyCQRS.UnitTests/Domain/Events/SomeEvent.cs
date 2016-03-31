using System;
using MyCQRS.Events;

namespace MyCQRS.UnitTests.Domain.Events
{
    public class SomeEvent : DomainEvent
    {
        public string Name { get; }

        public SomeEvent(Guid aggregateId, string name) : base(aggregateId)
        {
            Name = name;
        }
    }
}