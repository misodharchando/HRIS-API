using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace HRISWebAPI.Api.Services;

public class TokenService(IConfiguration c)
{
    public string Create(string id)
    {
        var k = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(c["Jwt:Key"]!));
        var t = new JwtSecurityToken(
            c["Jwt:Issuer"],
            c["Jwt:Audience"],
            [new Claim("sub", id)],
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(k, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(t);
    }
}
