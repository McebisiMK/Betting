using Betting.Application.Interfaces;
using Betting.Common.Configurations;
using Betting.Infrastructure.Persistence;
using Betting.Infrastructure.RabbitMQ;
using Betting.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Betting.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AssessmentDatabase");
            services.AddDbContext<BettingDbContext>(builder => builder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));

            services.AddOptions();
            services.Configure<RabbitMQConfiguration>(configuration.GetSection(nameof(RabbitMQConfiguration)));

            services.AddSingleton(serviceProvider =>
            {
                return new ConnectionFactory
                {
                    HostName = configuration.GetValue<string>("RabbitMQConfiguration:Server"),
                    UserName = configuration.GetValue<string>("RabbitMQConfiguration:Username"),
                    Password = configuration.GetValue<string>("RabbitMQConfiguration:Password")
                };
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMessagePublisher, RabbitMQPublisher>();

            return services;
        }
    }
}
