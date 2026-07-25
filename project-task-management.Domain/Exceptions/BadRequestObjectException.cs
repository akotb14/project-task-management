namespace project_task_management.Domain.Exceptions
{
    public class BadRequestObjectException : Exception
    {
        public BadRequestObjectException(string? message) : base(message)
        {
        }
    }
}
