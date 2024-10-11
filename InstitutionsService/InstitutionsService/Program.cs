using InstitutionsService.Data;
using InstitutionsService.Data.Interceptors;
using InstitutionsService.Middlewares;
using InstitutionsService.RabbitMQ;
using InstitutionsService.Services;
using InstitutionsService.Services.Impl;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllCors",
                       policy =>
                       {
                           policy.AllowAnyOrigin() 
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                       });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BaseEntityInterceptor>();
builder.Services.AddScoped<InstitutionInterceptor>();
builder.Services.AddScoped<InstitutionReplicationInterceptor>();
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemoryDbName")!);
    var baseEntityInterceptor = serviceProvider.GetRequiredService<BaseEntityInterceptor>();
    var institutionInterceptor = serviceProvider.GetRequiredService<InstitutionInterceptor>();
    var institutionReplicationInterceptor = serviceProvider.GetRequiredService<InstitutionReplicationInterceptor>();
    options.AddInterceptors(baseEntityInterceptor);
    options.AddInterceptors(institutionInterceptor);
    options.AddInterceptors(institutionReplicationInterceptor);
});

//Add services
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<IInstitutionService, InstitutionService>();
builder.Services.AddScoped<IInstitutionReplicationService, InstitutionReplicationService>();
builder.Services.AddScoped<IClassifierService, ClassifierService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseRouting();
app.UseMiddleware<ResponseLoggingMiddleware>();
app.UseCors("AllCors");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    //Create test data on startup
    var scopedContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DataSeed.Run(scopedContext);

    // create queues on startup
    var rabbitMqClient = app.Services.GetRequiredService<IRabbitMqClient>();
    var classifierService = scope.ServiceProvider.GetRequiredService<IClassifierService>();
    await RabbitMqInitializer.Run(rabbitMqClient, classifierService);
}


await app.RunAsync();
