using LogicLayer.Handlers.Interfaces;
using ModelLayer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LogicLayer.Handlers
{
    public class MQHandler : IDisposable, IMQHandler
    {
        private ConnectionFactory? _connection;
        private string Message { get; set; } = "";
        public string _Message => this.Message;
        public void FactoryConnection(MQConfigurations configs)
        {
            if (string.IsNullOrEmpty(configs.URL))
                throw new ArgumentNullException(nameof(configs));


            _connection = new ConnectionFactory()
            {
                Uri = new Uri(configs.URL)
            };
        }
        public IModel ChannelCreated()
        {
            if (_connection == null)
                throw new InvalidOperationException("No connection setted");

            //Queue declare
            using var connection = _connection.CreateConnection();
            var channel = connection.CreateModel();

            return channel;
        }
        public void QueueDeclare(ref IModel channel, MQConfigurations configs)
        {

            channel.QueueDeclare(queue: configs.Queue,
                                    durable: configs.QueueConfigs.Durable,
                                    exclusive: configs.QueueConfigs.Exclusive,
                                    autoDelete: configs.QueueConfigs.AutoDelete,
                                    arguments: configs.QueueConfigs.Arguments);
        }
        public void ExchangeDeclare(ref IModel channel, MQConfigurations configs, string? exchangeType = null)
        {
            channel.ExchangeDeclare(exchange: configs.Exchange, type: string.IsNullOrWhiteSpace(exchangeType) ?
                                                                        ExchangeType.Direct : exchangeType);
        }
        public void BasicPublich(ref IModel channel, MQConfigurations configs)
        {
            channel.BasicPublish(exchange: configs.Exchange,
                                    routingKey: configs.RoutingKey,
                                    basicProperties: null,
                                    body: configs.Body);
        }
        public void Listener(ref IModel channel, MQConfigurations configs)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                this.Message = Encoding.UTF8.GetString(body);

            };
            channel.BasicConsume(queue: configs.Queue,
                                 autoAck: true,
                                 consumer: consumer);
        }
        public void CloseChannel(ref IModel channel)
        {
            if (channel != null || channel.IsOpen)
            {
                channel.Close();
                channel.Dispose();
            }
        }
        public void Dispose()
        {
            _connection = null;

        }
    }
}

