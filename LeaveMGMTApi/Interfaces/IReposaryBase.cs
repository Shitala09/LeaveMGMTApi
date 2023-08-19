using LeaveMGMTApi.Models;

namespace LeaveMGMTApi.Interfaces
{
    public interface IReposaryBase
    {
        Task<List<Users>> Users(UserReq user);

        Task<ReturnStatus> AddLeave(LeaveRequest leaveRequest);

        Task<List<LeaveRequest>> GetLeaves(string userId);
    }
}
