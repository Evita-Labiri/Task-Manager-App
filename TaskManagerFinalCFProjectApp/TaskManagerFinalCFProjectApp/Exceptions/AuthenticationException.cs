namespace TaskManagerFinalCFProjectApp.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base ("The authentication was not succesfull. Please check the inserted values and try again."){ }

        public AuthenticationException(string message) : base(message) { }
    }
}
