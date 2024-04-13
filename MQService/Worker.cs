using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace MQService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {


                var factory = new ConnectionFactory()
                {
                    Uri = new Uri("amqps://gmnszqwi:gjz---76F7gHIX_ePzOkyo0U44t91PiR@whale.rmq.cloudamqp.com/gmnszqwi")
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                // Declarar una cola
                channel.QueueDeclare(queue: "mi-cola",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Publicar un mensaje en la cola
                var message = "Hola, mundo!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "mi-cola",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine("Mensaje enviado: {0}", message);

                // Consumir mensajes de la cola
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Mensaje recibido: {0}", message);
                };
                channel.BasicConsume(queue: "mi-cola",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Presiona [Enter] para salir.");
                Console.ReadLine();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
