namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BookStore.Properties.Models;
    using BookStore.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;


        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var result = _productService.Get();
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); ;
            }
        }

        //// GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return StatusCode(200, product);
        }

        //// PUT: api/Products/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                var result = _productService.Put(product);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(201)]
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _productService.Post(product);
                    return StatusCode(201, result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var result = _productService.Delete(id);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {

                return NotFound(ex);
            }
        }
    }
}
