namespace OpenTelemetryExample.Core.Models
{
    public interface IOperationResult<T> : IOperationResult
    {
        T Data { get; set; }
    }

    public interface IOperationResult
    {
        bool Success { get; set; }
        Exception Exception { get; set; }
        string Message { get; set; }
        string Header { get; set; }
        int Code { get; set; }
        long Took { get; set; }
    }
}
