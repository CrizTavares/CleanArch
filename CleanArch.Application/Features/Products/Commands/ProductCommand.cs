using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Features.Products.Commands
{
    public abstract class ProductCommand : IRequest<Product>
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; } = String.Empty;
        public int CategoryId { get; set; }
    }
}
