namespace Application;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponse<T> Success(T data, string? message = null) => new()
    {
        IsSuccess = true,
        Data = data,
        Message = message,
    };

    public static ApiResponse<T> Error(List<string> errors, string? message = null) => new()
    {
        IsSuccess = false,
        Message = message,
        Errors = errors,
    };

    public static ApiResponse<T> Error(string error, string? message = null) => new()
    {
        IsSuccess = false,
        Message = message,
        Errors = [error]
    };
}