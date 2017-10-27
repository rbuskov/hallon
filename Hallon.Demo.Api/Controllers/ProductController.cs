using System;
using System.Web.Http;
using Hallon.Demo.Data;
using Hallon.Demo.Resources;
using Hallon.Demo.Services;

namespace Hallon.Demo.Controllers
{
    [RoutePrefix("api")]
    public class ProductController : DemoController<Product, ProductResource>
    {
        private readonly ProductService service;

        public ProductController()
            => service = new ProductService();

        // Get

        [HttpGet, Route("products")]
        public IHttpActionResult Get()
            => Handle(service.Get());

        [HttpGet, Route("products/{id}")]
        public IHttpActionResult Get(int id)
            => Handle(service.Get(id));

        // Post, Put, Delete

        [HttpPost, Route("products")]
        public IHttpActionResult Create(ProductRequest request)
            => Handle(service.Create(request));

        [HttpPut, Route("products/{id}")]
        public IHttpActionResult Update(ProductRequest request)
            => Handle(service.Update(request));

        [HttpDelete, Route("products/{id}")]
        public IHttpActionResult Delete(int id)
            => Handle(service.Delete(id));
    }
}
