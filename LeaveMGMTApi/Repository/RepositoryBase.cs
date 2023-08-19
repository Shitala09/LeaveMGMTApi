using LeaveMGMTApi.Data;
using LeaveMGMTApi.Interfaces;
using LeaveMGMTApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LeaveMGMTApi.Repository
{
    public class RepositoryBase: IReposaryBase
    {
        private readonly DBContext _context;
        public RepositoryBase(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> Users(UserReq user)
        {
            var parameters = new[]
            {
                    new SqlParameter("@USERNAME", user.UserName),
                    new SqlParameter("@PASSWORD", user.Password)
            };
            List<Users> users = await _context.Users.FromSqlRaw("exec proc_validate_user @USERNAME, @PASSWORD", parameters.ToArray()).ToListAsync();
            return users;

        }


        public async Task<ReturnStatus> AddLeave(LeaveRequest leaveRequest)
        {
            var parameters = new[]
   {
                new SqlParameter("@EMPID",leaveRequest.EmployeeId),
                new SqlParameter("@EMPNAME",leaveRequest.EmployeeName),
                new SqlParameter("@REASON",leaveRequest.Reason),
                new SqlParameter("@STARTDATE", leaveRequest.StartDate),
                new SqlParameter("@ENDDATE",leaveRequest.EndDate)
            };
            var results = await _context.Database.ExecuteSqlRawAsync("exec proc_Insert_Leave @EMPID,@EMPNAME,@REASON,@STARTDATE,@ENDDATE", parameters.ToArray());
            return new ReturnStatus(1, "Success");

        }

        public async Task<List<LeaveRequest>> GetLeaves(string userId)
        {
            var parameters = new[]
           {
                    new SqlParameter("@USERID", userId),
  
            };
            List<LeaveRequest> leaveRequests = await _context.LeaveRequests.FromSqlRaw("exec proc_get_leaves @USERID", parameters.ToArray()).ToListAsync();
            return leaveRequests;

        }
    }
}
