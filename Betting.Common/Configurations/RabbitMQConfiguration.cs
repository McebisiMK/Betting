namespace Betting.Common.Configurations
{
    public class RabbitMQConfiguration
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Server { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
        public string Exchange { get; set; } = string.Empty;
    }
}
