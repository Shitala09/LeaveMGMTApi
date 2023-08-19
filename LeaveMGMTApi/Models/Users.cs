namespace LeaveMGMTApi.Models
{
    public class Users
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string Emailid { get; set; }
        public string MobileNo { get; set; }
        public string EmpId { get; set; }
        public string Pwd { get; set; }
        public decimal? Status { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDt { get; set; }

        public string? SecurityToken { get; set; }
    }

    public class UserReq
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
