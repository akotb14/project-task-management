namespace project_task_management.Domain.Exceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string? message) : base(message)
        {
        }
    }
}
