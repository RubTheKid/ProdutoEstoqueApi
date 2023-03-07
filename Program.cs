using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProdutoEstoqueApi.Context;
using ProdutoEstoqueApi.Models;
using ProdutoEstoqueApi.Services;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => 
                                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? SqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                                    options.UseSqlServer(SqlServerConnection));

///*---------jwt-----------*/

//builder.Services.AddSingleton<ITokenService>(new TokenService());

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

///*---------jwt-----------*/


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
//app.UseAuthentication();

app.UseCors();

app.MapControllers();


app.Run();
