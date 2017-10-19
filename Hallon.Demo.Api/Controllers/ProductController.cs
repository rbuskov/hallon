using System.Linq;
using System.Web.Http;
using Hallon.Demo.Resources;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class ProductController : ApiController
    {
        [HttpGet, Route("products/{id}")]
        public ProductResource Get(int id)
        {
            return Repository.Products.FirstOrDefault(product => product.Id == id);
        }
    }
}
