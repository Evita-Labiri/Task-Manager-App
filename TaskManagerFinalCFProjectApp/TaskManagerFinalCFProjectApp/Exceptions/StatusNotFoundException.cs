namespace TaskManagerFinalCFProjectApp.Exceptions
{
    public class StatusNotFoundException : Exception
    {
        public StatusNotFoundException() : base("The status is not available") { }

        public StatusNotFoundException(string message) : base(message) { }

    }
}
