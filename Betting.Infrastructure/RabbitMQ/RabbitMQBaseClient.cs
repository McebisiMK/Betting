using Betting.Common.Configurations;
using RabbitMQ.Client;

namespace Betting.Infrastructure.RabbitMQ
{
    public class RabbitMQBaseClient : IDisposable
    {
        protected IModel? Channel { get; private set; }
        private IConnection? _connection;
        private readonly ConnectionFactory _connectionFactory;
        private readonly RabbitMQConfiguration _rabbitMQConfiguration;

        public RabbitMQBaseClient(ConnectionFactory connectionFactory, RabbitMQConfiguration rabbitMQConfiguration)
        {
            _connectionFactory = connectionFactory;
            _rabbitMQConfiguration = rabbitMQConfiguration;

            CreateRabbitMQConnection();
        }

        private void CreateRabbitMQConnection()
        {
            if (NoEstablishedConnection())
            {
                _connection = _connectionFactory.CreateConnection();
            }

            if (NoEstablishedChannel())
            {
                Channel = _connection!.CreateModel();
                Channel.QueueDeclare(queue: _rabbitMQConfiguration.QueueName, durable: false, exclusive: false, autoDelete: false);
            }
        }

        private bool NoEstablishedChannel()
        {
            return Channel == null || Channel.IsOpen == false;
        }

        private bool NoEstablishedConnection()
        {
            return _connection == null || _connection.IsOpen == false;
        }

        public void Dispose()
        {
            Channel?.Close();
            Channel?.Dispose();
            Channel = null;

            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
    }
}
