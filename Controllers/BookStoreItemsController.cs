namespace BookStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BookStore.Properties.Models;
    using BookStore.Interfaces;
    using AutoMapper;
    using BookStore.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreItemsController : ControllerBase
    {
        private readonly IBookStoreService _bookstoreService;

        private readonly IMapper _mapper;

        public BookStoreItemsController(IBookStoreService bookStoreService, IMapper mapper)
        {
            _bookstoreService = bookStoreService;
            _mapper = mapper;
        }

        // GET: api/BookStoreItems
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetBookStoreItems()
        {
            var result = _mapper.Map<List<BookStoreItem>>(_bookstoreService.Get());

            if (!ModelState.IsValid)
                return BadRequest();

            return StatusCode(200, result);
        }

        // GET: api/BookStoreItems/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(BookStoreItem))]
        public  IActionResult GetBookStoreItem(int id)
        {
            if (!_bookstoreService.Exists(id))
                return NotFound();

            var store = _mapper.Map<BookStoreItemDto>(_bookstoreService.Get(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return StatusCode(200, store);
        }

        // PUT: api/BookStoreItems/5
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult PutBookStoreItem(int id, BookStoreItemDto updatedStore)
        {
            if (updatedStore == null)
                return BadRequest(ModelState);

            if (id != updatedStore.Id)
                return BadRequest(ModelState);

            if (!_bookstoreService.Exists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var storeMap = _mapper.Map<BookStoreItem>(updatedStore);

            if (!_bookstoreService.Put(storeMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating BookStore");
                return StatusCode(500, ModelState);
            }
            return StatusCode(204, "Book Store was updated");
        }

        // POST: api/BookStoreItems
        [ProducesResponseType(201)]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult PostBookStoreItem(BookStoreItemDto createStore)
        {
            if (createStore == null)
                return BadRequest(ModelState);

            var store = _mapper.Map<List<BookStoreItemDto>>(_bookstoreService.Get()).Where(b => b.Id == createStore.Id).FirstOrDefault();

            if (store != null)
            {
                ModelState.AddModelError("", "Store with same Id exists");
                return StatusCode(422, ModelState);
            }

            var storeMap = _mapper.Map<BookStoreItem>(createStore);
            if (!_bookstoreService.Post(storeMap))
            {
                ModelState.AddModelError("", "Something went wrong, Store item was not saved");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201, "Store was created");
        }

        // DELETE: api/BookStoreItems/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBookStoreItem(int id)
        {
            if (!_bookstoreService.Exists(id))
                return NotFound();

            var storeToDelete = _bookstoreService.Get(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bookstoreService.Delete(storeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting store item");
            }

            return StatusCode(204, "Store was deleted");
        }
    }
}
