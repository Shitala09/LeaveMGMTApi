namespace LeaveMGMTApi.Models
{
    public class RequestData<T> where T : class
    {
        public T Request { get; set; }

    }
}
