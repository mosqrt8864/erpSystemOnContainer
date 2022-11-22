using MaterialsManagement.Api.Infrastructure.AutofacModules;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using MaterialsManagement.Api.Errors;
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => 
{
    builder.RegisterModule(new ApplicationModule());
    builder.RegisterModule(new InfrastructureModule());
});

// Logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
  .WriteTo.Console()
  .CreateLogger();
builder.Host.UseSerilog(logger);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddProblemDetails(
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseExceptionHandler();
app.UseStatusCodePages();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseDeveloperExceptionPage();
}
else
{
  app.UseExceptionHandler();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
