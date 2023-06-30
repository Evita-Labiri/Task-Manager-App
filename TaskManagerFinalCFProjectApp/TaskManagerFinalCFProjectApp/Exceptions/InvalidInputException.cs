namespace TaskManagerFinalCFProjectApp.Exceptions
{
    public class InvalidInputException :Exception
    {
        public InvalidInputException() : base("Invalid input provided.") { }

        public InvalidInputException(string message) : base(message)
        {
        }
    }
}
