using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Hallon.Demo.Data;

namespace Hallon.Demo.Services
{
    public class DemoService<T> where T : DemoEntity
    {
        private readonly IList<T> repository;

        protected DemoService(IList<T> repository) 
            => this.repository = repository;

        // Get

        public ServiceResult<IEnumerable<T>> Get()
            => new ServiceResult<IEnumerable<T>>(repository);

        public ServiceResult<T> Get(int id)
            => GetSingle(entity => entity.Id == id);

        protected ServiceResult<T> GetSingle(Func<T, bool> expression)
        {
            switch (repository.SingleOrDefault(expression))
            {
                case null:
                    return ServiceResult<T>.NotFound();

                case T entity:
                    return new ServiceResult<T>(entity);
            }
        }
        protected ServiceResult<IEnumerable<T>> GetMany(Func<T, bool> expression)
            => new ServiceResult<IEnumerable<T>>(repository.Where(expression));

        // Create

        protected ServiceResult<T> Create<TRequest, TValidator>(TRequest request, Func<T> func) 
            where TValidator : AbstractValidator<TRequest>, new()
        {
            var validator = new TValidator();

            switch (validator.Validate(request))
            {
                case ValidationResult result when !result.IsValid:                  
                    return new ServiceResult<T>(result.Errors);

                default:
                    return Create(func);
            }
        }

        protected ServiceResult<T> Create(Func<T> func)
        {
            var entity = func.Invoke();

            repository.Add(entity);

            return new ServiceResult<T>(entity);
         }

        // Update

        protected ServiceResult<T> Update<TRequest, TValidator>(int id, TRequest request, Action<T> action)
            where TValidator : AbstractValidator<TRequest>, new()
        {
            var validator = new TValidator();

            switch (validator.Validate(request))
            {
                case ValidationResult result when !result.IsValid:
                    return new ServiceResult<T>(result.Errors);

                default:
                    return Update(id, action);
            }
        }

        protected ServiceResult<T> Update<TValidator>(int id, Action<T> action)
            where TValidator : AbstractValidator<int>, new()
            => Update<int, TValidator>(id, id, action);

        protected ServiceResult<T> Update(int id, Action<T> action)
        {
            var serviceResult = Get(id);

            if (serviceResult.Success)
                action.Invoke(serviceResult.Value);

            return serviceResult;
        }

        // Delete

        protected ServiceResult<T> DeleteById<TValidator>(int id) 
            where TValidator : AbstractValidator<int>, new()
        {
            var validator = new TValidator();

            switch (validator.Validate(id))
            {
                case ValidationResult result when !result.IsValid:
                    return new ServiceResult<T>(result.Errors);

                default:
                    return DeleteById(id);
            }
        }

        protected ServiceResult<T> DeleteById(int id)
        {
            var result = Get(id);

            if (!result.Success)
                return result;

            repository.Remove(result.Value);

            return new ServiceResult<T>(default(T));
        }
    }
}