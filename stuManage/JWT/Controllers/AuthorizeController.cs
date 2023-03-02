using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using Result;
using Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly CustomerService _CustomerService;
        public AuthorizeController(CustomerService CustomerService)
        {
            _CustomerService = CustomerService;
        }

        [HttpPost("Login")]
        public async Task<ApiResult> Login([FromBody] Helper value)
        {
            Customer data = await _CustomerService.FindItem(c => c.id == value.id && c.password == value.password);

            if (data != null)
            {
                // 登录成功
                var claims = new Claim[]
                {
                    new Claim("id", data.id.ToString()),
                    new  Claim("id", data.name.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GBTR-OEUR1-DCS-UYTGR-SDFGTRE-ES"));
                var token = new JwtSecurityToken(
                    issuer: MySetting.Mysetting.issuerURL,
                    audience: MySetting.Mysetting.audienceURL,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(10),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                Dictionary<string, string> result = new Dictionary<string, string>();
                result.Add("id", data.id.ToString());
                result.Add("token", jwtToken);
                return ApiResultHelper.Success(result);

            }
            else
            {
                return ApiResultHelper.Error("Incorrect account or password");
            }

        }
    }

}
