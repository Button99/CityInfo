using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CityInfo.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        public class AuthenticationRequestBody
        {
            public string? username { get; set; }
            public string? password { get; set; }
        }

        [HttpPost("authenticate")]
        public ActionResult<string> authenticate(AuthenticationRequestBody authenticationRequestBody) {
            var user = ValidateUserCredentials(
                authenticateRequestBody.username,
                authenticationRequestBody.password);

            if(user == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes('1234'));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            // Claim user data name pass etc.
            claimsForToken.Add(new Claim("sub", user.userId.ToString()));

            var jwtSecurityToken = new JwtSecurityToken('issuer', 'audience', claimsForToken,
                DateTime.UtcNow, DateTime.UtcNow.AddHours(1), signingCredentials);

            var tokenReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenReturn);

        }

        private object validateUserCredentials(string? username, string? password)
        {

        }
    }
}
