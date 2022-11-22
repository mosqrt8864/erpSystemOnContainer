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
builder.Services.AddProblemDetails(options =>
        options.CustomizeProblemDetails = (context) =>
        {
            var partNumberErrorFeature = context.HttpContext.Features
                                                       .Get<PartNumberErrorFeature>();
            if (partNumberErrorFeature is not null)
            {
                (string Detail, string Type) details = partNumberErrorFeature.PartNumberError switch
                {
                    PartNumberErrorType.SameKeyError =>
                    ("Id已存在於資料庫",
                                          ""),
                    PartNumberErrorType.EmptyKeyError =>
                    ("Id不可為空白",
                                          ""),
                    PartNumberErrorType.NotExistKeyError =>
                    ("Id不存在",
                                          ""),
                    _ => ("其他錯誤",
                                          "")
                };

                context.ProblemDetails.Type = details.Type;
                //context.ProblemDetails.Title = "輸入參數錯誤";
                context.ProblemDetails.Detail = details.Detail;
            }
        }
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseExceptionHandler("/error-development");
}
else
{
  app.UseExceptionHandler("/error");
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
