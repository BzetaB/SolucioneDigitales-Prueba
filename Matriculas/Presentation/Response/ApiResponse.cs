namespace Matriculas.Presentation.Response
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }

        public ApiResponse(bool isSuccess, T data = default, string message = "" )
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }

        public static ApiResponse<T> Success(T data, string message = "")
        {
            return new ApiResponse<T>(true, data, message);
        }

        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>(false, default, message);
        }
    }
}
