using Hotel.API.Backgrounds;
using Hotel.API.Filters;
using Hotel.BussinessLogic.Commands;
using Hotel.DataAccess.Context;
using Hotel.Shared.Dispatchers;
using Hotel.Shared.Messaging;
using Hotel.Shared.Payments.Stripe;
using Hotel.Shared.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFilters();

builder.Services.AddSql();
builder.Services.AddRedis();
builder.Services.AddDispatcher();
builder.Services.AddMessaging();
builder.Services.AddStripeCheckout();

builder.Services.AddHostedService<AppInitializer>();
builder.Services.AddHostedService<StreamingService>();
builder.Services.AddHostedService<MessagingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRedisStreaming()
    .SubscribeAsync<SendNotificationCommand>("email");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
//app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
