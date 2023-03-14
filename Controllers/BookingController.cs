namespace BookStore.Controllers
{
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models;
using AutoMapper;
using BookStore.Interfaces;
using BookStore.DTO;
using BookStore.Enums;

    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }


        // GET: Booking
        public IActionResult Index(string searchString)
        {
            if (_bookingService.Get() == null)
            {
                return Problem("Booking list is empty.");
            }

            var bookings = from b in _bookingService.Bookings()
                           select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b => b.Product!.Name.Contains(searchString));
            }
            return View(bookings.ToList());
        }

        // GET: Booking/Details/5
        public IActionResult Details(int id)
        {
            if (id == null || _bookingService.Get(id) == null)
            {
                return NotFound();
            }

            var booking = _bookingService.Get(id);

            return View(booking);
        }

        // GET: Booking/Create
        public IActionResult Create(int id)
        {
            ViewData["Product"] = _bookingService.GetProducts().Where(p => p.Id == id).Select(p => p.Name).FirstOrDefault();
            ViewData["Price"] = _bookingService.GetProducts().Where(p => p.Id == id).Select(p => p.Price).FirstOrDefault();
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Product, [Bind("Delivery_Adress,Delivery_date,Delivery_Time")] BookingDto booking)
        {
            booking.ProductId = _bookingService.GetProducts().Where(p => p.Name == Product).FirstOrDefault().Id;
            booking.StatusId = (int)Statuses.SUBMITED;
            if (ModelState.IsValid)
            {
                var bookingMap = _mapper.Map<Booking>(booking);
                bookingMap.Status = _bookingService.GetStatuses().Where(s => s.Id == (int)Statuses.SUBMITED).FirstOrDefault();
                bookingMap.Product = _bookingService.GetProducts().Where(p => p.Name == Product).FirstOrDefault();
                bookingMap.Product.Bookings.Add(bookingMap);
                if (!_bookingService.Post(bookingMap))
                {
                    ModelState.AddModelError("", "Something went wrong, booking was not saved");
                    return StatusCode(500, ModelState);
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Product"] = _bookingService.GetProducts().Where(p => p.Id == booking.ProductId).Select(p => p.Name).FirstOrDefault();
            ViewData["Price"] = _bookingService.GetProducts().Where(p => p.Id == booking.ProductId).Select(p => p.Price).FirstOrDefault();
            return View(booking);
        }

        // GET: Booking/Edit/5
        public IActionResult Edit(int id)
        {
            var booking = _bookingService.Get(id);
            if (id == null || booking == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_bookingService.GetProducts(), "Id", "Id", booking.ProductId);
            ViewData["StatusId"] = new SelectList(_bookingService.GetStatuses(), "Id", "Id", booking.StatusId);
            ViewData["UserId"] = new SelectList(_bookingService.GetUsers(), "Id", "Id", booking.UserId);
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Delivery_Adress,Delivery_date,Delivery_Time,UserId,StatusId,ProductId")] BookingDto bookingUpdate)
        {
            if (ModelState.IsValid)
            {
                if (bookingUpdate == null)
                    return BadRequest(ModelState);

                if (id != bookingUpdate.Id)
                    return BadRequest(ModelState);

                if (!_bookingService.Exists(id))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var bookingMap = _mapper.Map<Booking>(bookingUpdate);

                if (!_bookingService.Put(bookingMap))
                {
                    ModelState.AddModelError("", "Something went wrong while updating booking");
                    return StatusCode(500, ModelState);
                }
                return RedirectToAction(nameof(Index));
            }
            var booking = _bookingService.Get(id);
            ViewData["ProductId"] = new SelectList(_bookingService.GetProducts(), "Id", "Id", booking.ProductId);
            ViewData["StatusId"] = new SelectList(_bookingService.GetStatuses(), "Id", "Id", booking.StatusId);
            ViewData["UserId"] = new SelectList(_bookingService.GetUsers(), "Id", "Id", booking.UserId); ;
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateApprovedStatus(string id)
        {
            var Id = Convert.ToInt16(Request.Form["id"]);
            var booking = _bookingService.Get(Id);
            booking.StatusId = (int)Statuses.APPROVED;

                if (!_bookingService.Put(booking))
                {
                    ModelState.AddModelError("", "Something went wrong while updating booking");
                    return StatusCode(500, ModelState);
                }

            return RedirectToAction(nameof(Index)); 
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateRejectStatus(string id)
        {
            var Id = Convert.ToInt16(Request.Form["id"]);
            var booking = _bookingService.Get(Id);
            booking.StatusId = (int)Statuses.REJECTED;

            if (!_bookingService.Put(booking))
            {
                ModelState.AddModelError("", "Something went wrong while updating booking");
                return StatusCode(500, ModelState);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCompleteStatus(string id)
        {
            var Id = Convert.ToInt16(Request.Form["id"]);
            var booking = _bookingService.Get(Id);
            booking.StatusId = (int)Statuses.COMPLETED;

            if (!_bookingService.Put(booking))
            {
                ModelState.AddModelError("", "Something went wrong while updating booking");
                return StatusCode(500, ModelState);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Booking/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = _bookingService.Get(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var bookingToDelete = _bookingService.Get(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bookingService.Delete(bookingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting booking");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
