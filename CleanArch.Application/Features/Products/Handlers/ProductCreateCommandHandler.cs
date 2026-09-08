using CleanArch.Application.Features.Products.Commands;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Validation;
using MediatR;

namespace CleanArch.Application.Features.Products.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.Image);
            if (product == null)
            {
                throw new DomainExceptionValidation($"Error creating entity");
            }
            else
            {
                product.CategoryId = request.CategoryId;
                return _productRepository.CreateAsync(product);
            }

        }
    }
}

