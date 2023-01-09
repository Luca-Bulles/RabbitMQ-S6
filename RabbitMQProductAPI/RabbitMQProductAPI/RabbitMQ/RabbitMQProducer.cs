using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQProductAPI.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            //Specify the RabbitMQ Server
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Create RabbitMQ connection
            var connection = factory.CreateConnection();
            //Create channel with session and model
            using var channel = connection.CreateModel();
            //Declare the queue
            channel.QueueDeclare("product", exclusive: false);
            //Serialze the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //Put data in the product queue
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);
        }
    }
}
