namespace BookStore.Controllers
{

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models;
using AutoMapper;
using BookStore.Interfaces;
using BookStore.DTO;
using BookStore.Enums;

    public class BookStoreItemController : Controller
    {
        private readonly IBookStoreService _bookStoreService;

        private readonly IMapper _mapper;

        public BookStoreItemController(IBookStoreService bookStoreService, IMapper mapper)
        {
            _bookStoreService = bookStoreService;
            _mapper = mapper;
        }

        // GET: BookStoreItem
        public IActionResult Index(string searchString)
        {
            if (_bookStoreService.Get() == null)
            {
                return Problem("Stores list is empty.");
            }

            var stores = from s in _bookStoreService.BookStoreItems()
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                stores = stores.Where(s => s.Product.Name!.Contains(searchString));
            }
            return View(stores.ToList());
        }

        // GET: BookStoreItem/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _bookStoreService.Get(id) == null)
            {
                return NotFound();
            }

            var bookStoreItem = _bookStoreService.Get(id);

            return View(bookStoreItem);
        }

        // GET: BookStoreItem/Create
        public IActionResult Create()
        {
            ViewData["Name"] = new SelectList(_bookStoreService.Products(), "Name", "Name");
            return View();
        }

        // POST: BookStoreItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Product, [Bind("Available,Booked,Sold")] BookStoreItemDto bookStoreItem)
        {
            var bookings = _bookStoreService.ProductAsIQ()
                 .Where(b => b.Name == Product)
                 .SelectMany(b => b.Bookings)
                 .Where(i => i.StatusId == (int)Statuses.APPROVED ||
                 i.StatusId == (int)Statuses.SUBMITED
                 || i.StatusId == (int)Statuses.COMPLETED);

            var bookedCount = bookings.Where(s => s.StatusId == (int)Statuses.APPROVED).Count();
            var soldCount = bookings.Where(s => s.StatusId == (int)Statuses.COMPLETED).Count();

            bookStoreItem.ProductId = _bookStoreService.ProductAsIQ().Where(p => p.Name == Product).FirstOrDefault().Id;
            bookStoreItem.Booked = bookedCount;
            bookStoreItem.Sold = soldCount;
            bookStoreItem.Available = bookStoreItem.Available - (bookStoreItem.Booked + bookStoreItem.Sold);

            if (ModelState.IsValid)
            {
                var storeMap = _mapper.Map<BookStoreItem>(bookStoreItem);
                if (!_bookStoreService.Post(storeMap))
                {
                    ModelState.AddModelError("", "Something went wrong, store item was not saved");
                    return StatusCode(500, ModelState);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Name"] = new SelectList(_bookStoreService.Products(), "Name", "Name");
            return View(bookStoreItem);
        }

    
        public IActionResult Edit(int id)
        {
            if (id == null || _bookStoreService.Get(id) == null)
            {
                return NotFound();
            }
            _bookStoreService.Products().SelectMany(p => p.Bookings).Where(b => b.StatusId == (int)Statuses.SUBMITED || b.StatusId == (int)Statuses.APPROVED);
            var bookStoreItem = _bookStoreService.Get(id);

            if (bookStoreItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = bookStoreItem.Product.Id;
            ViewData["ProductName"] = bookStoreItem.Product.Name;
            ViewData["ProductBooked"] = bookStoreItem.Product.Bookings.Where(b => b.StatusId == (int)Statuses.SUBMITED || b.StatusId == (int)Statuses.APPROVED).Count();
            ViewData["ProductSold"] = bookStoreItem.Product.Bookings.Where(b => b.StatusId == (int)Statuses.COMPLETED).Count();
            return View(bookStoreItem);
        }

        // POST: BookStoreItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Available,Booked,Sold,ProductId")] BookStoreItemDto bookStoreItemUpdate)
        {
            bookStoreItemUpdate.Available = bookStoreItemUpdate.Available - (bookStoreItemUpdate.Booked + bookStoreItemUpdate.Sold);

            if (ValidateBookStoreItem(id, bookStoreItemUpdate))
            {
                var itemMap = _mapper.Map<BookStoreItem>(bookStoreItemUpdate);

                if (!_bookStoreService.Put(itemMap))
                {
                    ModelState.AddModelError("", "Something went wrong while updating store item");
                    return StatusCode(500, ModelState);
                }
                return RedirectToAction(nameof(Index));
            }
            var bookStoreItem = _bookStoreService.Get(id);
            ViewData["ProductId"] = bookStoreItem.Product.Id;
            ViewData["ProductName"] = bookStoreItem.Product.Name;
            return View(bookStoreItem);
        }

        protected bool ValidateBookStoreItem(int id, BookStoreItemDto bookStoreItemUpdate)
        {
            if (id != bookStoreItemUpdate.Id)
                ModelState.AddModelError("Id", "Id does not correspond item id.");
            if (bookStoreItemUpdate == null)
                ModelState.AddModelError("BookStoreItem", "BookStoreItem is null.");
            if (!_bookStoreService.Exists(id))
                ModelState.AddModelError("Bookstore Item is not found", "There is no such bookstore item.");
            return ModelState.IsValid;
        }

        // GET: BookStoreItem/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookStoreItem = _bookStoreService.Get(id);
            if (bookStoreItem == null)
            {
                return NotFound();
            }

            return View(bookStoreItem);
        }

        // POST: BookStoreItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var bookingToDelete = _bookStoreService.Get(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bookStoreService.Delete(bookingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting booking");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
