using Kibernetik.Data.DataUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kibernetik.Services
{
    public interface IJwtTocenService
    {
        string CreateToken(User user);
    }

    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }

    // Ключ для проверки подписи (публичный)
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }

    public class JwtTokenService : IJwtTocenService
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public JwtTokenService(IConfiguration configuration,
            UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public string CreateToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, authRequest.Name)
            };

            // 3. Генерируем JWT.
            var token = new JwtSecurityToken(
                issuer: "DemoApp",
                audience: "DemoAppClient",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;


            //var roles = _userManager.GetRolesAsync(user).Result;
            //List<Claim> claims = new List<Claim>()
            //{
            //    new Claim("name", user.login)
            //};

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim("roles", role));
            //}

            //var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<String>("JwtKey")));
            //var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            //var jwt = new JwtSecurityToken(
            //    signingCredentials: signinCredentials,
            //    expires: DateTime.Now.AddMinutes(1),
            //    claims: claims
            //);
            //return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
