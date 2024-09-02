using Betting.Application.Commands.CasinoWagers.Consume;
using Betting.Common.Configurations;
using Betting.Infrastructure.RabbitMQ;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Betting.Worker
{
    public class RabbitMQConsumerService : RabbitMQConsumer, IHostedService
    {
        private readonly RabbitMQConfiguration rabbitMQConfiguration;

        public RabbitMQConsumerService(IServiceScopeFactory serviceScopeFactory, ConnectionFactory connectionFactory, IOptions<RabbitMQConfiguration> options)
            : base(serviceScopeFactory, connectionFactory, options)
        {
            rabbitMQConfiguration = options.Value;

            var consumer = new AsyncEventingBasicConsumer(Channel);
            consumer.Received += OnEventReceived<ConsumeCasinoWagerCommand>;
            Channel.BasicConsume(queue: rabbitMQConfiguration.QueueName, autoAck: false, consumer: consumer);
        }

        public virtual Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}
