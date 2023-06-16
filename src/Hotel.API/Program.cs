using Hotel.API.Backgrounds;
using Hotel.API.Filters;
using Hotel.BusinessLogic;
using Hotel.BusinessLogic.Commands;
using Hotel.Shared.Dispatchers;
using Hotel.Shared.Logging;
using Hotel.Shared.MailKit;
using Hotel.Shared.Monitoring;
using Hotel.Shared.Redis;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Hotel.DataAccess;
using Hotel.API.Middleware;
using Hotel.Shared.Payments;
using Hotel.Shared.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddFilters();

builder.Services.AddRedis();
builder.Services.AddDispatcher();
builder.Services.AddMailKit();
builder.Services.AddPayment();
builder.Services.AddDataAccessLayer();
builder.Services.AddBusinessLogicLayer();
builder.Services.AddHostedService<AppInitializer>();
builder.Services.AddJwtAuthentication();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
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
    .SubscribeAsync<SendNotificationCommand>("email")
    .SubscribeAsync<InvoiceExpirationCommand>("__keyevent@0__:expired");

app.UseStaticFiles();

app.UseHttpsRedirection();
// app.UseJwtAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
