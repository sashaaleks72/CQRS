using Amazon.S3;
using Application;
using Infrastructure;
using Infrastructure.Configurations;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.Configure<AmazonS3Configuration>(builder.Configuration.GetSection("AWS:Bucket"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlers();
//app.UseAuthorization();

app.MapControllers();

app.Run();
