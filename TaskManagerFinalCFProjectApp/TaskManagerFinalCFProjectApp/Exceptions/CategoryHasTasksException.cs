namespace TaskManagerFinalCFProjectApp.Exceptions
{
    public class CategoryHasTasksException : Exception
    {
        public CategoryHasTasksException() : base("This category has task(s) and it can't be deleted. Please remove the categories task(s) and try again.") { }

        public CategoryHasTasksException(string message) : base(message) { }

        public CategoryHasTasksException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
