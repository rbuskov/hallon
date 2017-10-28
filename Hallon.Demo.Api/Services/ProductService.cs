using Hallon.Demo.Data;
using Hallon.Demo.Common;
using Hallon.Demo.Services.Validators;

namespace Hallon.Demo.Services
{
    public class ProductService : DemoService<Product>
    {
        public ProductService() : base(Repository.Products)
        { }

        public ServiceResult<Product> Create(ProductRequest request)
            => Create<ProductRequest, ProductRequestValidator>(request, () => new Product
            {
                Id = Repository.NextProductId,
                Name = request.Name,
                Price = request.Price
            });

        public ServiceResult<Product> Update(int id, ProductRequest request)
            => Update<ProductRequest, ProductRequestValidator>(id, request, (product) =>
            {
                product.Name = request.Name;
                product.Price = request.Price; 
            });

        public ServiceResult<Product> Delete(int id)
            => DeleteById<DeleteProductValidator>(id);
    }
}