using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProdutoEstoqueApi.Models;
using SwaggerUIwithJWTsupport.ViewModel;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace SwaggerUIwithJWTsupport.Controllers;

[Authorize]
[ApiController]
[Route("")]
public class LoginController : ControllerBase
{
    private readonly UserCurrent[] _users =
    {
        new UserCurrent {Password = "senha123", Username = "jadercardoso",
            FirstName = "Jader", Claim = new Claim("Home", "Default")},
        new UserCurrent { Username = "admin", Password = "admin",
            FirstName = "Admin", Claim = new Claim("Home", "Admin")}
    };
    private AuthConfiguration _confAuth = new AuthConfiguration().GetInstance();
    [Authorize(Roles = "Admin")]
    [HttpGet("role-admin")]
    public ActionResult Admin()
    {
        return Ok();
    }
    [Authorize(Roles = "Default")]
    [HttpGet("role-default")]
    public ActionResult Default()
    {
        return Ok();
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult Login(Usuarios usuario)
    {
        if (!_users.Any(a => a.Password == usuario.Password &&
                            a.Username == usuario.Username)) return BadRequest();
        var user = new UserCurrent
        {
            Username = usuario.Username,
            Password = usuario.Password,
        };
        return Ok(new
        {
            Success = true,
            Token = GenerateToke(user).Result
        });

    }

    private async Task<string> GenerateToke(UserCurrent user)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Username));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Username));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        if (user.Username == "admin")
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }
        claims.Add(new Claim(ClaimTypes.Role, "Default"));
        claims.Add(user.Claim);
        var calimIdetity = new ClaimsIdentity(claims);
        var tokeHandler = new JwtSecurityTokenHandler();

        var token = tokeHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _confAuth.GetIssuer(),
            Audience = _confAuth.GetAudience(),
            Subject = calimIdetity,
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials =
                new SigningCredentials(_confAuth.GetKey(),
                    SecurityAlgorithms.HmacSha256Signature)
        });

        return await Task.FromResult(tokeHandler.WriteToken(token));
    }
}