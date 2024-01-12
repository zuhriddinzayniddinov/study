using System.Text;
using AuthApi.Extensions;
using Entity.Models.ExternalAPIs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:1001");
builder.ConfigureDefault();
builder.Services.AddConfig(builder.Configuration);
ExternalAPI.EImzo = builder.Configuration.GetValue<string>("Params:EimzoApi");
ExternalAPI.OneID = builder.Configuration.GetValue<string>("Params:OneID");
// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddService();


var app = builder.Build();
await app.ConfigureDefault();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();