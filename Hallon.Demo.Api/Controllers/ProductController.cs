using System.Linq;
using System.Net;
using System.Web.Http;
using Hallon.Demo.Data;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class ProductController : ApiController
    {
        [HttpGet, Route("products/{id}")]
        public ProductResource Get(int id)
        {
            return Repository.Products.FirstOrDefault(p => p.Id == id)?.ToResource()
                ?? throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}
