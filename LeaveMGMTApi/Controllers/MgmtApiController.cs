using LeaveMGMTApi.Data;
using LeaveMGMTApi.Helpers;
using LeaveMGMTApi.Interfaces;
using LeaveMGMTApi.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
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

        public MgmtApiController(DBContext context, IReposaryBase repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public string Get()
        {
            return "Service is running !!";
        }

        [HttpPost(Name = "Login")]
        public async Task<ActionResult<ResultData<List<Users>>>> Login([FromBody] RequestData<UserReq> request)
        {
            List<Users> user = await _repository.Users(request.Request);

             if (user.Count>0)
            {
                user[0].SecurityToken = CreateJwtToken(user[0]);
                return new ResultData<List<Users>> ()
                {
                    Result = user,
                    Status = new ReturnStatus(1)
                };
            }
            else
            {
                return new ResultData<List<Users>>()
                {
                    Result = null,
                    Status = new ReturnStatus(-1)
                };
            }

        }


        public string CreateJwtToken(Users userLogin)
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


        [HttpPost(Name = "InsertLeave")]
        public async Task<ActionResult<ReturnStatus>> InsertLeave([FromBody] RequestData<LeaveRequest> request)
        {
            var result = await _repository.AddLeave(request.Request);
            return result;
        }

        [HttpPost(Name = "GetLeaveList")]
        public async Task<ActionResult<ResultData<List<LeaveRequest>>>> GetLeaveList([FromBody] RequestData<string> request)
        {
            var result = await _repository.GetLeaves(request.Request);
            return new ResultData<List<LeaveRequest>>()
            {
                Result = result,
                Status = new ReturnStatus(1)
            };
        }
    }
}
