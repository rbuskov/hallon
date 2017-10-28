using System.Collections.Generic;
using FluentValidation.Results;

namespace Hallon.Demo.Services
{
    public class ServiceResult<T> where T : class
    {
        private IList<ValidationFailure> errors;

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

        public ServiceResult(IList<ValidationFailure> errors)
        {
            this.errors = errors;
        }

        public static ServiceResult<T> NotFound()
            => new ServiceResult<T>($"{typeof(T).Name} not found.");

        public static ServiceResult<T> CustomerNotFound() 
            => new ServiceResult<T>("Customer not found.");

        public static ServiceResult<T> ProductNotFound()
            => new ServiceResult<T>("Product not found.");

        public static ServiceResult<T> OrderNotFound(int id)
            => new ServiceResult<T>("Order not found.");
    }
}