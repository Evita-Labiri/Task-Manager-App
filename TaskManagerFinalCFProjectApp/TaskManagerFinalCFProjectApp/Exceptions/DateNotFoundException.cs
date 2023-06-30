namespace TaskManagerFinalCFProjectApp.Exceptions
{
    public class DateNotFoundException : Exception
    {
        public DateNotFoundException() : base ("Due Date is not available"){ }

        public DateNotFoundException(string message) : base(message) { }

    }
}
