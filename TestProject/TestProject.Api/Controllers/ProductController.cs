using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Api.Response;
using TestProject.Core.Entities;
using TestProject.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _maper;
        public ProductController(IProductRepository postRepository, IMapper mapper)
        {
            _productRepository = postRepository;
            _maper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Getproducts()
        {
            var productdto = await _productRepository.GetProducts();
            var productdtodto = _maper.Map<IEnumerable<ProductDTOs>>(productdto);
            return Ok(productdtodto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getproduct(int id)
        {
            var productdto = await _productRepository.GetProduct(id);
            var productdtodto = _maper.Map<ProductDTOs>(productdto);
            return Ok(productdtodto);
        }
        [HttpPost]
        public async Task<IActionResult> Postproductdto(ProductDTOs product)
        {
            var productdto = _maper.Map<Produc>(product);
            await _productRepository.RegisterProduct(productdto);
            return Ok(productdto);
        }
        [HttpPut]
        public async Task<IActionResult> Putproductdto(int id, ProductDTOs productdto)
        {
            var productdtodto = _maper.Map<Produc>(productdto);
            productdtodto.Id = id;
            var Update = await _productRepository.UpdateProduct(productdtodto);
            var updateproductdto = new ApiResponse<bool>(Update);
            return Ok(updateproductdto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproductdto(int id)
        {

            var result = await _productRepository.DeleteProduct(id);
            var deleteproductdto = new ApiResponse<bool>(result);
            return Ok(deleteproductdto);
        }
    }
}
