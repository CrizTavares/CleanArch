using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Features.Products.Commands;
using CleanArch.Application.Features.Products.Queries;
using CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class ProductsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            IEnumerable<Product> products = await _mediator.Send(new GetProductsQuery());

            if (products == null)
            {
                return NotFound("Products not found");
            }

            IEnumerable<ProductDto> productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            Product product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product == null)
            {
                return NotFound("Product not found");
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("Data Invalid");

            ProductCreateCommand command = _mapper.Map<ProductCreateCommand>(productDto);
            Product createdProduct = await _mediator.Send(command);

            ProductDto createdProductDto = _mapper.Map<ProductDto>(createdProduct);

            return CreatedAtRoute("GetProduct", new { id = createdProductDto.Id }, createdProductDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            if (productDto == null || id != productDto.Id)
            {
                return BadRequest("Data invalid");
            }

            ProductUpdateCommand command = _mapper.Map<ProductUpdateCommand>(productDto);
            Product updatedProduct = await _mediator.Send(command);

            ProductDto updatedProductDto = _mapper.Map<ProductDto>(updatedProduct);
            return Ok(updatedProductDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDto>> Delete(int id)
        {
            ProductRemoveCommand command = new ProductRemoveCommand(id);
            Product deletedProduct = await _mediator.Send(command);

            if (deletedProduct == null)
            {
                return NotFound("Product not found");
            }

            ProductDto productDto = _mapper.Map<ProductDto>(deletedProduct);
            return Ok(productDto);
        }
    }
}