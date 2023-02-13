namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BookStore.Properties.Models;
    using BookStore.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreItemsController : ControllerBase
    {
        private readonly IBookStoreService _bookstoreService;

        public BookStoreItemsController(IBookStoreService bookStoreService)
        {
            _bookstoreService = bookStoreService;
        }

        // GET: api/BookStoreItems
        [HttpGet]
        public IActionResult GetBookStoreItems()
        {
            try
            {
                var result = _bookstoreService.Get();
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); ;
            }
        }

        // GET: api/BookStoreItems/5
        [HttpGet("{id}")]
        public  IActionResult GetBookStoreItem(int id)
        {
            var product = _bookstoreService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return StatusCode(200, product);
        }

        // PUT: api/BookStoreItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutBookStoreItem(int id, BookStoreItem bookStoreItem)
        {
            if (id != bookStoreItem.Id)
            {
                return BadRequest();
            }
            try
            {
                var result = _bookstoreService.Put(bookStoreItem);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/BookStoreItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(201)]
        [HttpPost]
        public IActionResult PostBookStoreItem(BookStoreItem bookStoreItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _bookstoreService.Post(bookStoreItem);
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

        // DELETE: api/BookStoreItems/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBookStoreItem(int id)
        {
            try
            {
                var result = _bookstoreService.Delete(id);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {

                return NotFound(ex);
            }
        }
    }
}
