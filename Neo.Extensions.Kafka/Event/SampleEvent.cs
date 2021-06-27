using System;
using Neo.Extensions.Kafka.Abstractions;

namespace Neo.Extensions.Kafka.Event
{
    public class SampleEvent : IEvent
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; }
    }
}
