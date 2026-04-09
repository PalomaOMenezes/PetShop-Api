using Petshop.Api;
using Petshop.Api.Interface;
using Petshop.Api.Repository;
using Petshop.Api.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Petshop.Api", Version = "v1" });

    // 1. DEFINE O BOTÃO "AUTHORIZE"
    // Isso diz ao Swagger que existe uma forma de autenticação via Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Cabeçalho de autorização JWT usando o esquema Bearer.
                      \r\n\r\n Digite 'Bearer' [espaço] e depois seu token na caixa de texto abaixo.
                      \r\n\r\nExemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // 2. APLICA A SEGURANÇA NOS ENDPOINTS
    // Isso coloca o cadeadinho em cima de cada método da API
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
builder.Services.AddSingleton<IClienteService, ClienteService>();

builder.Services.AddSingleton<IPetRepository, PetRepository>();
builder.Services.AddSingleton<IPetService, PetService>();

builder.Services.AddSingleton<IResumoServicoRepository, ResumoServicoRepository>();
builder.Services.AddSingleton<IResumoServicoService, ResumoServicoService>();

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UGFsb21hIGRlIE9saXZlaXJhIE1lbmV6ZXM")),

        // Adiciona estas linhas:
        ValidateIssuer = true,
        ValidIssuer = "yourApp",

        ValidateAudience = true,
        ValidAudience = "yourApi"
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() // Libera todas as origens
                  .AllowAnyHeader() // Libera todos os headers
                  .AllowAnyMethod(); // Libera todos os métodos (GET, POST, etc)
        });
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllers();

app.Run();
