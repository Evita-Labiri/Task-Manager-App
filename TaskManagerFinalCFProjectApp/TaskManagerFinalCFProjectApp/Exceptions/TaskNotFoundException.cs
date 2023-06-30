namespace TaskManagerFinalCFProjectApp.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException() : base("The task was not found.") { }

        public TaskNotFoundException(string message) : base(message) { }
    }
}
