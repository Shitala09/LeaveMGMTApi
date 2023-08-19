using LeaveMGMTApi.Data;
using LeaveMGMTApi.Helpers;
using LeaveMGMTApi.Interfaces;
using LeaveMGMTApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LeaveMGMTApi.Controllers
{
    [Route("[controller]/[action]")]
    public class MgmtApiController : Controller
    {
        private readonly DBContext _context;
        public readonly IReposaryBase _repository;

        public MgmtApiController(DBContext context, IReposaryBase _repository)
        {
            _context = context;

        }

        [HttpGet]
        public string Get()
        {
            return "Service is running !!";
        }


        public string CreateJwtToken(UserLoginInfo userLogin)
        {
            string stkey = ConfigHelper.GetConfigStr("Jwt:Key");
            string issuer = ConfigHelper.GetConfigStr("Jwt:Issuer");
            string audience = ConfigHelper.GetConfigStr("Jwt:Audience");
            int expMin = ConfigHelper.GetConfig<int>("Jwt:ExpiryTimeInMin", 1440);

            var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(stkey));
            var credential = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            claims.Add(new Claim("data", userLogin.ToString()));
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, expires: DateTime.Now.AddMinutes(expMin), signingCredentials: credential);
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;
        }
    }
}
