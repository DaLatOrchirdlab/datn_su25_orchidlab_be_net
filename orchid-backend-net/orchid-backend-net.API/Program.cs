using orchid_backend_net.API.Configuration;
using orchid_backend_net.API.Filters;
using orchid_backend_net.Application;
using orchid_backend_net.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Register application and infrastructure services
builder.Services.AddApplication(builder.Configuration);
builder.Services.ConfigureApplicationSecurity(builder.Configuration);
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureProblemDetails();
builder.Services.ConfigureSwagger(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.ConfigurationCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
