namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BookStore.Models;
    using AutoMapper;
    using BookStore.Interfaces;
    using BookStore.DTO;

    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: Product
        public IActionResult Index(string productName)
        {
            if (_productService.Get() == null)
            {
                return Problem("Users list is empty.");
            }

            var products = from p in _productService.Products()
                        select p;

            if (!String.IsNullOrEmpty(productName))
            {
                products = products.Where(u => u.Name!.Contains(productName));
            }
            return View(products.ToList());
        }

        // GET: Product/Details/5
        public IActionResult Details(int id)
        {
            if (id == null || _productService.Get(id) == null)
            {
                return NotFound();
            }

            var product = _productService.Get(id);

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Author,Price,Image_Path")] ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var productMap = _mapper.Map<Product>(product);
                if (!_productService.Post(productMap))
                {
                    ModelState.AddModelError("", "Something went wrong, product was not saved");
                    return StatusCode(500, ModelState);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _productService.Get(id) == null)
            {
                return NotFound();
            }

            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Author,Price,Image_Path")] ProductDto productUpdate)
        {
            if (ModelState.IsValid)
            {
                if (productUpdate == null)
                    return BadRequest(ModelState);

                if (id != productUpdate.Id)
                    return BadRequest(ModelState);

                if (!_productService.Exists(id))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productMap = _mapper.Map<Product>(productUpdate);

                if (!_productService.Put(productMap))
                {
                    ModelState.AddModelError("", "Something went wrong while updating product");
                    return StatusCode(500, ModelState);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productUpdate);
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var productToDelete = _productService.Get(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productService.Delete(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting product");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
