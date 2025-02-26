using AzureEntraIDDemo.Application.Interfaces;
using AzureEntraIDDemo.Application.Services;
using AzureEntraIDDemo.Domain.Interfaces;
using AzureEntraIDDemo.Infrastructure.Repositories;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Azure AD
builder.Services.AddMicrosoftIdentityWebApiAuthentication(
    builder.Configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi() // Habilita a aquisição de tokens para chamar APIs downstream
    //.AddMicrosoftGraph(builder.Configuration.GetSection("GraphApi")) // Adiciona suporte ao Microsoft Graph
    .AddInMemoryTokenCaches(); // Armazena tokens em cache na memória

// Registrar o GraphServiceClient
builder.Services.AddScoped(sp =>
{
    var tokenAcquisition = sp.GetRequiredService<ITokenAcquisition>();
    var scopes = builder.Configuration.GetSection("GraphApi:Scopes").Get<string[]>();
    var authProvider = new GraphAuthenticationProvider(tokenAcquisition, scopes);
    return new GraphServiceClient(authProvider);
});

// Registrar o GraphRepository
builder.Services.AddScoped<IGraphRepository, GraphRepository>();

// Registrar o UserService
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();