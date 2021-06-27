using System;

namespace Neo.Extensions.Kafka.Abstractions
{
    public interface IEventProvider
    {
        void RegisterEvent(params (string key, Type type)[] events);

        Type GetByKey(string key);
    }
}
