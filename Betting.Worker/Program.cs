using Betting.Application;
using Betting.Application.Helpers;
using Betting.Application.Helpers.Interceptors;
using Betting.Application.Interfaces;
using Betting.Common.Configurations;
using Betting.Infrastructure;
using Betting.Infrastructure.Persistence;
using Betting.Infrastructure.RabbitMQ;
using Betting.Infrastructure.Repositories;
using Betting.Worker;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<RabbitMQConsumerService>();

var connectionString = builder.Configuration.GetConnectionString("AssessmentDatabase");
builder.Services.AddDbContext<BettingDbContext>(builder => builder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));

builder.Services.AddTransient<IValidatorInterceptor, RequestValidatorInterceptor>();
builder.Services.AddMediatR(typeof(IBettingApplicationAssemblyHelper).Assembly);
builder.Services.AddAutoMapper(typeof(IBettingApplicationAssemblyHelper).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(IBettingApplicationAssemblyHelper).Assembly);

builder.Services.AddOptions();
builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection(nameof(RabbitMQConfiguration)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMessagePublisher, RabbitMQPublisher>();


builder.Services.AddSingleton(serviceProvider =>
{
    return new ConnectionFactory
    {
        HostName = builder.Configuration.GetValue<string>("RabbitMQConfiguration:Server"),
        UserName = builder.Configuration.GetValue<string>("RabbitMQConfiguration:Username"),
        Password = builder.Configuration.GetValue<string>("RabbitMQConfiguration:Password"),
        DispatchConsumersAsync = true
    };
});


var host = builder.Build();
host.Run();
