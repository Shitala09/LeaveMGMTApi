using System.Text.Json;

namespace LeaveMGMTApi.Middlewares
{
    public class ErrorDetails
    {
        public int ErrorCode { set; get; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize<ErrorDetails>(this);
        }
    }
}
