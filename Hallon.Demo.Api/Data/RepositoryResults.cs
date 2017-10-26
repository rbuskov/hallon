namespace Hallon.Demo.Data
{

    public interface IRepositoryResult { }

    public class Success<T> : IRepositoryResult
    {
        public T Value { get; }

        public Success(T value)
        {
            Value = value;
        }
    }

    public class Failure : IRepositoryResult
    {
        public string Message { get; }

        public Failure(string message)
        {
            Message = message;
        }
    }
}