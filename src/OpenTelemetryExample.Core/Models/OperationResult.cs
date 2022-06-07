namespace OpenTelemetryExample.Core.Models
{
    [Serializable]
    public class OperationResult<T> : OperationResult, IOperationResult<T>
    {
        private T _data;

        public OperationResult(bool success)
            : base(success, string.Empty, 900)
        {
        }

        public OperationResult(T data)
            : base(true, string.Empty, 200)
        {
            Data = data;
        }
        public OperationResult(T data, string message)
            : base(true, string.Empty, 200)
        {
            Data = data;
            Message = message;
        }

        public OperationResult(bool success, string message, int code, Exception ex = null, string header = null)
            : base(success, message, code, ex, header)
        {
            Data = default(T);
            Success = success;
        }

        public OperationResult(bool success, KeyValuePair<int, string> resourceConstant, string header = null)
            : base(success, resourceConstant.Value, resourceConstant.Key, null, header)
        {

        }

        public OperationResult(bool success, T data, KeyValuePair<int, string> resourceConstant, string header = null)
            : base(success, resourceConstant.Value, resourceConstant.Key, null, header)
        {
            Data = data;
            Success = success;
        }

        public OperationResult(T data, KeyValuePair<int, string> resourceConstant)
            : base(true, resourceConstant.Value, resourceConstant.Key)
        {
            Data = data;
        }

        public OperationResult()
        {

        }
        public T Data
        {
            get => _data;
            set
            {
                Success = true;
                _data = value;
            }
        }
    }
    [Serializable]
    public class OperationResult : IOperationResult
    {
        public OperationResult(bool success, string message, int code, Exception ex = null, string header = null)
        {
            Success = success;
            Message = message;
            Header = header;
            Exception = ex;
            if (!success && code == 0)
                Code = 500;
            else
                Code = code;
        }

        public OperationResult(bool success, KeyValuePair<int, string> resourceConstant)
            : this(success, resourceConstant.Value, resourceConstant.Key)
        {
        }

        public OperationResult(bool success)
            : this(success, string.Empty, 200)
        {
        }

        public OperationResult()
        {

        }

        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public string Header { get; set; }
        private int _code = 200;
        public int Code
        {
            get => _code;
            set => _code = value;
        }
        public long Took { get; set; }
    }
}
