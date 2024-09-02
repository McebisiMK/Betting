using Betting.Common.Configurations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Betting.Infrastructure.RabbitMQ
{
    public class RabbitMQConsumer : RabbitMQBaseClient
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQConsumer(IServiceScopeFactory serviceScopeFactory, ConnectionFactory connectionFactory, IOptions<RabbitMQConfiguration> options)
            : base(connectionFactory, options.Value)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected virtual async Task OnEventReceived<TEntity>(object sender, BasicDeliverEventArgs @event)
        {
            var mediator = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<IMediator>();

            var message = Encoding.UTF8.GetString(@event.Body.ToArray());
            var casinoWager = JsonConvert.DeserializeObject<TEntity>(message);

            await mediator.Send(casinoWager);
            Channel!.BasicAck(@event.DeliveryTag, false);
        }
    }
}
