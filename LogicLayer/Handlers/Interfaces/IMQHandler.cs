using ModelLayer;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Handlers.Interfaces
{

    public interface IMQHandler
    {
        public interface IMQHandler
        {
            string _Message { get; }
            void FactoryConnection(MQConfigurations configs);
            IModel ChannelCreated();
            void QueueDeclare(ref IModel channel, MQConfigurations configs);
            void ExchangeDeclare(ref IModel channel, MQConfigurations configs, string? exchangeType = null);
            void BasicPublich(ref IModel channel, MQConfigurations configs);
            void Listener(ref IModel channel, MQConfigurations configs);
            void CloseChannel(ref IModel channel);
        }
    }
}

