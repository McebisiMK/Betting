using Betting.Application.Interfaces;
using Betting.Common.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Betting.Infrastructure.RabbitMQ
{
    public class RabbitMQPublisher : RabbitMQBaseClient, IMessagePublisher
    {
        private readonly RabbitMQConfiguration rabbitMQConfiguration;

        public RabbitMQPublisher(ConnectionFactory connectionFactory, IOptions<RabbitMQConfiguration> options) : base(connectionFactory, options.Value)
        {
            rabbitMQConfiguration = options.Value;
        }

        public void Publish<TMessage>(TMessage message)
        {
            var properties = Channel!.CreateBasicProperties();
            properties.DeliveryMode = 1;
            properties.ContentType = "application/json";

            var messageJson = JsonConvert.SerializeObject(message);
            var serializedMessage = Encoding.UTF8.GetBytes(messageJson);

            Channel.BasicPublish(exchange: rabbitMQConfiguration.Exchange, routingKey: rabbitMQConfiguration.QueueName, body: serializedMessage);
        }
    }
}
