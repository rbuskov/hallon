using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Hallon.Demo.Data;
using Hallon.Demo.Services;

namespace Hallon.Demo.Controllers
{
    public abstract class DemoController<TEntity, TResource, TSummaryResource> : ApiController 
        where TEntity : DemoEntity
        where TResource : Resource
        where TSummaryResource : Resource
    {
        protected IHttpActionResult Handle(ServiceResult<TEntity> result) 
            => result.Success
                ? Ok(Mapper.Map<TResource>(result.Value)) as IHttpActionResult
                : BadRequest(result.ErrorMessage);

        protected IHttpActionResult Handle(ServiceResult<IEnumerable<TEntity>> result)
            => result.Success
                ? Handle(result.Value)
                : BadRequest(result.ErrorMessage);

        protected IHttpActionResult Handle(IEnumerable<TEntity> collection)
            => Ok(collection.Select(item => Mapper.Map<TSummaryResource>(item)));
    }
}
