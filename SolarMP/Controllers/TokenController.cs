using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SolarMP.DTOs;
using SolarMP.DTOs.Account;
using SolarMP.DTOs.JWT;
using SolarMP.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SolarMP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;

        private readonly solarMPContext _context;

        public TokenController(IConfiguration configuration, solarMPContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        /// <summary>
        /// làm ơn là hãy đăng nhập gòi mới test các api
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]

        [Route("Login_username_password")]
        public async Task<IActionResult> getToken(LoginDTO dto)
        {
            try
            {
                if(dto == null)
                {
                    return BadRequest();
                }
                else
                {
                    // kiểm tra tài khoản
                    var acc = await this._context.Account.Where(x=>x.Username.Equals(dto.Username) && x.Status)
                        .Include(x=>x.Role)
                        .FirstOrDefaultAsync();
                    if(acc == null)
                    {
                        throw new Exception("Không tồn tại người dùng");
                    }
                    else
                    {
                        // kiểm tra mật khẩu
                        if (!(BCrypt.Net.BCrypt.Verify(dto.Password, acc.Password)))
                        {
                            throw new Exception("Mật khẩu không đúng!");
                        }
                        // kiểm tra staff có phải là lead không
                        //if(acc.RoleId == "3")
                        //{
                        //    if(!acc.IsLeader == true)
                        //    {
                        //        throw new Exception("Tài khoản staff không có quyền hạn đăng nhập vào hệ thống");
                        //    }
                        //}
                        string test = _configuration["Jwt:Subject"];
                        // jwt token gen
                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", acc.AccountId.ToString()),
                        new Claim("Username", acc.Username.ToString()),
                        new Claim("Email", acc.Email ?? "null"),
                        new Claim("Password", acc.Password),
                        new Claim(ClaimTypes.Role, acc.Role.RoleId.ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        //expires: DateTime.UtcNow.AddMinutes(30),
                        expires: DateTime.UtcNow.AddDays(7),
                        signingCredentials: signIn);

                        var result = new JWTRespone();
                        result.user = acc;
                        result.token = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(result);

                    }
                }
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
            return Unauthorized();
        }
    }
}
