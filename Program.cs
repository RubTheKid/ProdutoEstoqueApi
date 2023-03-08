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

//------------jwt-----------

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




//-------------jwt------------------

//builder.Services.AddControllers().AddJsonOptions(options => 
//options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


///*---------jwt-----------*/


///*---------jwt-----------*/

string? SqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                                    options.UseSqlServer(SqlServerConnection));

///*---------jwt-----------*/
///

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    o.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey
//        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = false,
//        ValidateIssuerSigningKey = true
//    };
//});


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


//builder.Services.AddAuthorization();

//builder.Services.AddSingleton<ITokenService>(new TokenService());

//builder.Services.AddAuthorization();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,

//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };

//});

//builder.Services.AddAuthorization();
/*---------jwt-----------*/

var app = builder.Build();

///*---------jwt-----------*/
////login endpoint




//app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
//{
//    if (userModel == null)
//    {
//        return Results.BadRequest("Login inválido");
//    }
//    if (userModel.Username == "admin" && userModel.Password == "admin")
//    {
//        var tokenString = tokenService.GetToken
//        (
//            app.Configuration["Jwt:Key"],
//            app.Configuration["Jwt:Issuer"],
//            app.Configuration["Jwt:Audience"],
//            userModel
//        );
//        return Results.Ok(new { token = tokenString });
//    }
//    else
//    {
//        return Results.BadRequest("Login inválido");
//    }
//}).Produces(StatusCodes.Status400BadRequest).Produces(StatusCodes.Status200OK).WithName("Login").WithTags("Autenticacao");



//app.MapPost("/login",
//[AllowAnonymous] (Usuarios user) =>
//{
    
//    if (user.Username == "admin" && user.Password == "admin")
//    {
//        var issuer = builder.Configuration["Jwt:Issuer"];
//        var audience = builder.Configuration["Jwt:Audience"];
//        var key = Encoding.ASCII.GetBytes
//        (builder.Configuration["Jwt:Key"]);
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//                new Claim("Id", Guid.NewGuid().ToString()),
//                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
//                new Claim(JwtRegisteredClaimNames.Email, user.Username),
//                new Claim(JwtRegisteredClaimNames.Jti,
//                Guid.NewGuid().ToString())
//             }),
//            Expires = DateTime.UtcNow.AddMinutes(5),
//            Issuer = issuer,
//            Audience = audience,
//            SigningCredentials = new SigningCredentials
//            (new SymmetricSecurityKey(key),
//            SecurityAlgorithms.HmacSha512Signature)
//        };
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        var jwtToken = tokenHandler.WriteToken(token);
//        var stringToken = tokenHandler.WriteToken(token);
//        return Results.Ok(stringToken);
//    }
//    return Results.Unauthorized();
//});


///*---------jwt-----------*/


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
