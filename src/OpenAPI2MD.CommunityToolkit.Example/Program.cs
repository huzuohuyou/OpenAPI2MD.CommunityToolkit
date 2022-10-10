using ApiConventions.CommunityToolKit.Extends;
using ApiConventions.CommunityToolKit.Log4Net;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.ReDoc;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenSetUp(new OpenApiContact
{
    Name = "wuhailong",
    Email = "13126506430@163.com"
});

builder.Services.AddFilterSetUp();
builder.Services.AddLog4Net();
builder.Services.AddAuthenticationSetUp(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetUp();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseReDoc(c =>
{
    c.RoutePrefix = "api-docs";
    c.SpecUrl = "https://localhost:18100/swagger/1.0.0/swagger.json";
    c.ConfigObject = new ConfigObject
    {
        HideDownloadButton = true,
        HideLoading = true
    };
});
app.Run();
