using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class MQConfigurations
    {
        public string? RoutingKey { get; set; }
        public string? Exchange { get; set; }
        public byte[]? Body { get; set; }
        public string? Queue { get; set; }
        public QueueConfigs QueueConfigs { get; set; } = new QueueConfigs();
        public string? URL { get;set; }
        public MQConfigurations(string? url = null, string? exchange = null,
                                string? routingKey = null, string? queue = null,
                                byte[]? body = null, QueueConfigs? queueConfigs = null)
        {
            URL = url;
            Exchange = exchange;
            RoutingKey = routingKey;
            Queue = queue;
            Body = body;
            QueueConfigs = queueConfigs == null? new QueueConfigs():queueConfigs;
        }

    }

    public class QueueConfigs
    {
        public bool Durable { get; set; } = false;
        public bool AutoDelete { get; set; }=false;
        public bool Exclusive { get; set; } = false;
        public IDictionary<string, object>? Arguments { get; set; }
    }

}
