using Hotel.API.Backgrounds;
using Hotel.API.Filters;
using Hotel.BusinessLogic;
using Hotel.BusinessLogic.Commands;
using Hotel.BusinessLogic.Services;
using Hotel.DataAccess.Context;
using Hotel.Shared.Dispatchers;
using Hotel.Shared.Lock;
using Hotel.Shared.Logging;
using Hotel.Shared.MailKit;
using Hotel.Shared.Messaging;
using Hotel.Shared.Monitoring;
using Hotel.Shared.Payments.Momo;
using Hotel.Shared.Payments.PayPal;
using Hotel.Shared.Payments.Stripe;
using Hotel.Shared.Redis;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFilters();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddSql();
builder.Services.AddRedis();
builder.Services.AddDispatcher();
//builder.Services.AddMailKit();
//builder.Services.AddMessaging();
//builder.Services.AddDistributedLock();
//builder.Services.AddMomoCheckout();
//builder.Services.AddPayPalCheckout();
//builder.Services.AddStripeCheckout();
//builder.Services.AddDataAccessLayer();
builder.Services.AddBusinessLogicLayer();
//builder.Services.AddHostedService<AppInitializer>();
//builder.Services.AddHostedService<StreamingService>();
//builder.Services.AddHostedService<MessagingService>();

// custom logging
builder.Host.UseLogging();
builder.Host.UseMonitoring();



builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRedisStreaming()
    .SubscribeAsync<SendNotificationCommandRejected>("email")
    .SubscribeAsync<SendNotificationCommand>("email", onError: (c, e) => new SendNotificationCommandRejected
    { Code = e.Code, Message = e.Message, Email = c.Email });

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
//app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
