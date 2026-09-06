using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Features.Products.Commands;
using CleanArch.Application.Features.Products.Queries;
using CleanArch.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArch.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(
            IMediator mediator,
            IMapper mapper,
            ICategoryService categoryService,
            IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _mapper = mapper;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return View(productsDto);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<ProductCreateCommand>(productDto);
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _mediator.Send(new GetProductByIdQuery(id.Value));
            if (product == null) return NotFound();

            var productDto = _mapper.Map<ProductDTO>(product);

            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<ProductUpdateCommand>(productDto);
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _mediator.Send(new GetProductByIdQuery(id.Value));
            if (product == null) return NotFound();

            var productDto = _mapper.Map<ProductDTO>(product);
            return View(productDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new ProductRemoveCommand(id));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _mediator.Send(new GetProductByIdQuery(id.Value));
            if (product == null) return NotFound();

            var productDto = _mapper.Map<ProductDTO>(product);

            var imagePath = Path.Combine(_environment.WebRootPath, "images", productDto.Image ?? string.Empty);
            ViewBag.ImageExist = System.IO.File.Exists(imagePath);

            return View(productDto);
        }
    }
}