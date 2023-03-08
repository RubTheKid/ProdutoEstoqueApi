using System.Security.Claims;

namespace SwaggerUIwithJWTsupport.ViewModel;

public class UserCurrent
{
    public string Password { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public Claim Claim { get; set; }
}