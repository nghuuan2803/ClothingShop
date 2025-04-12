namespace Shared.Responses
{
    public class BaseResponse
    {
        public string? RequestId { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int Code { get; set; }
        public object? Data { get; set; }
    }
}
