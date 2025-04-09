namespace Shared.Responses
{
    public class BaseResponse
    {
        public string RequestId { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public object Data { get; set; }
    }
}
