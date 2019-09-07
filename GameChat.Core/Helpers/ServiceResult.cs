namespace GameChat.Core.Helpers
{
    /// <summary>
    /// Indicates whether the action performed in service was successful and sends message in response
    /// </summary>
    public class ServiceResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public ServiceResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; private set; }

        public ServiceResult(bool success, string message) :
            base(success, message)
        {

        }

        public ServiceResult(bool success, string message, T data) :
            base(success, message)
        {
            Data = data;
        }
    }
}
