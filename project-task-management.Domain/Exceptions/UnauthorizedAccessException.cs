namespace project_task_management.Domain.Exceptions
{
    public class UnauthorizedAccessException : Exception

    {
        public UnauthorizedAccessException(string? message) : base(message)
        {
        }
    }
}
