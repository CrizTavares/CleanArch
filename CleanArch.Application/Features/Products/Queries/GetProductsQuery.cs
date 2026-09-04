using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Features.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
