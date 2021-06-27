using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Neo.Extensions.Kafka.Abstractions
{
    public interface IEvent : INotification
    {
    }
}
