using Hotel.BussinessLogic.Commands;
using Hotel.Shared.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseRedisStreaming()
//    .SubscribeAsync<SendNotificationCommand>("email")
//    .SubscribeAsync<OrderCreated>("orders");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
