using API.Utility;
using APPLICATION;
using INFRASTRUCTURE;
using INFRASTRUCTURE.Extensions;
using INFRASTRUCTURE.Invariant;
using INFRASTRUCTURE.Swagger;
using INFRASTRUCTURE.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddOptions();
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor()
        .ConfigureAppServices(builder.Environment)
        .AddApplication()
        .AddControllers()
        .AddNewtonsoftJson(JsonOptionsConfigure.ConfigureJsonOptions);
builder.Services.AddAppApiVersioning().AddSwagger(builder.Environment, builder.Configuration);

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

app.UseCors(x => x
             .SetIsOriginAllowed(origin => true)
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials());
app.ConfigureMiddlewareForEnvironments(builder.Environment);
app.UseHttpsRedirection();
app.UseMiddleware<JsonExceptionMiddleware>();
app.UseSwaggerInDevAndStaging(builder.Environment);
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Services.RunMi();
app.Run();

