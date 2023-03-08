using ProdutoEstoqueApi.Models;

namespace ProdutoEstoqueApi.Services;

public interface ITokenService
{
    string GetToken(string key, string issuer, string audience, Usuarios user);
}
