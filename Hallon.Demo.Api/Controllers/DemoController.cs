using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Hallon.Demo.Services;

namespace Hallon.Demo.Controllers
{
    public abstract class DemoController<TEntity, TResource> : ApiController
    {
        protected IHttpActionResult Handle(ServiceResult<TEntity> result) 
            => result.Success
                ? Ok(Mapper.Map<TResource>(result.Value)) as IHttpActionResult
                : BadRequest(result.ErrorMessage);

        protected IHttpActionResult Handle(IEnumerable<TEntity> collection) 
            => Ok(collection.Select(item => Mapper.Map<TResource>(item)));
    }
}
