namespace project_task_management.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string? message) : base(message)
        {
        }
    }
}
