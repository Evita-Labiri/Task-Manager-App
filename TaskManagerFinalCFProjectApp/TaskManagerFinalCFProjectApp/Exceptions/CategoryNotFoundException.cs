namespace TaskManagerFinalCFProjectApp.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException() : base("The categroy was not found"){ }

        public CategoryNotFoundException(string message) : base(message) { }
    }
}
