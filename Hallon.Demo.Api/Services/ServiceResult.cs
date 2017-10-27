using System.Web.Http.Results;
using System.Web.Management;
using Hallon.Demo.Data;

namespace Hallon.Demo.Services
{
    public class ServiceResult<T>
    {
        public bool Success { get; }

        public T Value { get; }

        public string ErrorMessage { get; set; }

        public ServiceResult(T value)
        {
            Value = value;
            Success = true;
        }

        public ServiceResult(string message)
        {
            ErrorMessage = message;
            Success = false;
        }

        public static ServiceResult<T> CustomerNotFound(int id) 
            => new ServiceResult<T>($"Customer with ID '{id}' not found.");

        public static ServiceResult<T> ProductNotFound(int productId)
        {
            throw new System.NotImplementedException();
        }

        public static ServiceResult<T> OrderNotFound(int productId)
        {
            throw new System.NotImplementedException();
        }
    }
}