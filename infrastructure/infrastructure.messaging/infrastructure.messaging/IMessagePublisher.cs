using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.messaging
{
    public interface IMessagePublisher
    {
        Task SendMessage<T>(T message) where T : class;
        Task ShutDown();
    }
}
