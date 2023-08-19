namespace LeaveMGMTApi.Models
{
    public class ResultData<T> where T : class
    {
        /// <summary>
        /// value hold the actual result,
        /// value will be valid if status code is zero
        /// </summary>
        public T Result { get; set; }

        ReturnStatus _Status;
        /// <summary>
        /// error message
        /// </summary>
        public ReturnStatus Status
        {
            get
            {
                if (_Status == null) { _Status = new ReturnStatus(); }
                return _Status;
            }
            set { _Status = value; }
        }
        public ResultData()
        {
            Status = new ReturnStatus();
        }

        public ResultData(T resultObj, int code, string message)
        {
            Result = resultObj;
            Status = new ReturnStatus(code);
        }

        public ResultData(T resultval, int statusCode = 0)
        {
            Result = resultval;
            Status = new ReturnStatus();
            Status.StatusCode = statusCode;
        }

        public void ClearStatus()
        {
            Status = new ReturnStatus(0);
        }
    }


    public class ReturnStatus
    {
        public ReturnStatus(int code, string msg = "")
        {
            StatusCode = code;
            Message = msg;
        }

        public ReturnStatus()
        {
            Message = "";
        }

        public void Clear()
        {
            StatusCode = 0;
        }

        public string Message { get; set; }
        /// <summary>
        /// request status code
        /// </summary>
        public int StatusCode { get; set; }

    }


    public class ResultData<TSuccess, TError>
    {
        public ReturnStatus Status { get; set; }
        public TSuccess SuccessData { get; set; }
        public TError ErrorData { get; set; }

        public ResultData()
        {
            Status = new ReturnStatus();
        }

        public ResultData(TSuccess resultObj, int code, string message)
        {
            SuccessData = resultObj;
            Status = new ReturnStatus(code);
        }

        public ResultData(TSuccess resultval, int statusCode = 0)
        {
            SuccessData = resultval;
            Status = new ReturnStatus();
            Status.StatusCode = statusCode;
        }
        public ResultData(TError resultObj, int code, string message)
        {
            ErrorData = resultObj;
            Status = new ReturnStatus(code);
        }

        public ResultData(TError resultval, int statusCode = 0)
        {
            ErrorData = resultval;
            Status = new ReturnStatus();
            Status.StatusCode = statusCode;
        }

    }
}
