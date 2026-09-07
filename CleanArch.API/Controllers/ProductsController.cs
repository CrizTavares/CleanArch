using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Features.Products.Commands;
using CleanArch.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _mediator.Send(new GetProductsQuery());

            if (products == null)
            {
                return NotFound("Products not found");
            }

            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product == null)
            {
                return NotFound("Product not found");
            }

            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Data Invalid");

            var command = _mapper.Map<ProductCreateCommand>(productDto);
            var createdProduct = await _mediator.Send(command);

            var createdProductDto = _mapper.Map<ProductDTO>(createdProduct);

            return CreatedAtRoute("GetProduct", new { id = createdProductDto.Id }, createdProductDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
        {
            if (productDto == null || id != productDto.Id)
            {
                return BadRequest("Data invalid");
            }

            var command = _mapper.Map<ProductUpdateCommand>(productDto);
            var updatedProduct = await _mediator.Send(command);

            var updatedProductDto = _mapper.Map<ProductDTO>(updatedProduct);
            return Ok(updatedProductDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var command = new ProductRemoveCommand(id);
            var deletedProduct = await _mediator.Send(command);

            if (deletedProduct == null)
            {
                return NotFound("Product not found");
            }

            var productDto = _mapper.Map<ProductDTO>(deletedProduct);
            return Ok(productDto);
        }
    }
}