using Hotel.API.Filters;
using Hotel.BussinessLogic.Commands;
using Hotel.Shared.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFilters();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseRedisStreaming()
//    .SubscribeAsync<SendNotificationCommand>("email", onError: (c, e) => new SendNotificationCommand());

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
//app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
