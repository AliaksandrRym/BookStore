namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BookStore.Properties.Models;
    using BookStore.Interfaces;
    using AutoMapper;
    using BookStore.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IMapper _mapper;


        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var result = _mapper.Map<List<ProductDto>>(_productService.Get());

            if (!ModelState.IsValid)
                return BadRequest();

            return StatusCode(200, result);
        }

        //// GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Product))]
        public IActionResult GetProduct(int id)
        {
            if (!_productService.Exists(id))
                return NotFound();

            var product = _mapper.Map<ProductDto>(_productService.Get(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return StatusCode(200, product);
        }

        //// PUT: api/Products/5
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult PutProduct(int id, ProductDto updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest(ModelState);

            if (id != updatedProduct.Id)
                return BadRequest(ModelState);

            if (!_productService.Exists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(updatedProduct);

            if (!_productService.Put(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while Product updating");
                return StatusCode(500, ModelState);
            }
            return StatusCode(204, "Product was updated");
        }

        // POST: api/Products
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult PostProduct(ProductDto createProduct)
        {
            if (createProduct == null)
                return BadRequest(ModelState);

            var product = _mapper.Map<List<ProductDto>>(_productService.Get())
                .Where(p => p.Name == createProduct.Name).FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product with same Name exists");
                return StatusCode(422, ModelState);
            }

            var productMap = _mapper.Map<Product>(createProduct);
            if (!_productService.Post(productMap))
            {
                ModelState.AddModelError("", "Something went wrong, product was not saved");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201, "Product was created");
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int id)
        {
            if (!_productService.Exists(id))
                return NotFound();

            var productToDelete = _productService.Get(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productService.Delete(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting product");
            }

            return StatusCode(204, "Product was deleted");
        }
    }
}
