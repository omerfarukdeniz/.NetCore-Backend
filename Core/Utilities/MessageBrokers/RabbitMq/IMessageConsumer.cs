using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.MessageBrokers.RabbitMq
{
    public interface IMessageConsumer
    {
        void GetQueue();
    }
}
