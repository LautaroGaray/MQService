using ModelLayer;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LogicLayer.Handlers
{
    public class MQServiceConfigurations : IDisposable
    {
        public void Dispose()
        {
            _connection = null;
        }
        private ConnectionFactory? _connection;
       
        }
        

    }
}
