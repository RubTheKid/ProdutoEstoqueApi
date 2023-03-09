using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ProdutoEstoqueApi.Context;
using ProdutoEstoqueApi.Models;
using ProdutoEstoqueApi.Services;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using SwaggerUIwithJWTsupport;

var builder = WebApplication.CreateBuilder(args);

var confAuth = new AuthConfiguration().GetInstance();

builder.Services.AddControllers();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = confAuth.GetKey(),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = confAuth.GetAudience(),
        ValidIssuer = confAuth.GetIssuer()
    };
});

builder.Services.AddEndpointsApiExplorer();

string? SqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                                    options.UseSqlServer(SqlServerConnection));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SwaggerUIWithJWTsupport",
        Description = "A parte da aplicação ligada a autenticação via JWT no swagger foi desenvolvida por Guilherme Malaquias e adaptada por mim. Obrigado Guilherme!",
        Contact = new OpenApiContact() { Name = "Guilherme Malaquias", Email = "silvaam.guilherme@gmail.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/Licenses/MIT") },
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta maneira: Bearer {seu token}",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.UseCors();

app.MapControllers();


app.Run();
