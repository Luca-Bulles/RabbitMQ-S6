namespace RabbitMQProductAPI.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        public void SendProductMessage <T>(T message);
    }
}
