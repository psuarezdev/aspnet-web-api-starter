using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetWebApiStarter.Models.DTOs.User;

namespace AspNetWebApiStarter.Libs
{
  public class Utils
  {
    private readonly IConfiguration _config;

    public Utils(IConfiguration config)
    {
      _config = config;
    }

    public string HashPassword(string password)
    {
      return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
      return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public string GenerateJwtToken(UserDTO user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(
        [
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Email, user.Email!),
        ]),
        Expires = DateTime.UtcNow.AddDays(5),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
